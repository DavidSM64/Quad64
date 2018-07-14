using Quad64.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Quad64
{
	public class MultiselectTreeView : TreeView
	{
		#region Selected Node(s) Properties

		private List<TreeNode> m_SelectedNodes = null;		
		public List<TreeNode> SelectedNodes
		{
			get
			{
				return m_SelectedNodes;
			}
			set
			{
				ClearSelectedNodes();
				if( value != null )
				{
					foreach( TreeNode node in value )
					{
						ToggleNode( node, true );
					}
				}
			}
		}

		// Note we use the new keyword to Hide the native treeview's SelectedNode property.
		private TreeNode m_SelectedNode;
		public new TreeNode SelectedNode
		{
			get { return m_SelectedNode; }
			set
			{
				ClearSelectedNodes();
				if( value != null )
				{
					SelectNode( value );
				}
			}
		}

		#endregion

		public MultiselectTreeView()
		{
			m_SelectedNodes = new List<TreeNode>();
			base.SelectedNode = null;
		}

		#region Overridden Events

		protected override void OnGotFocus( EventArgs e )
		{
			// Make sure at least one node has a selection
			// this way we can tab to the ctrl and use the 
			// keyboard to select nodes
			try
			{
				if( m_SelectedNode == null && this.TopNode != null )
				{
					ToggleNode( this.TopNode, true );
				}

				base.OnGotFocus( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnMouseDown( MouseEventArgs e )
		{
			// If the user clicks on a node that was not
			// previously selected, select it now.

			try
			{
				base.SelectedNode = null;

				TreeNode node = this.GetNodeAt( e.Location );
				if( node != null )
				{
					int leftBound = node.Bounds.X; // - 20; // Allow user to click on image
					int rightBound = node.Bounds.Right + 10; // Give a little extra room
					if( e.Location.X > leftBound && e.Location.X < rightBound )
					{
						if( ModifierKeys == Keys.None && ( m_SelectedNodes.Contains( node ) ) )
						{
							// Potential Drag Operation
							// Let Mouse Up do select
						}
						else
						{							
							SelectNode( node );
						}
					}
				}

				base.OnMouseDown( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnMouseUp( MouseEventArgs e )
		{
			// If the clicked on a node that WAS previously
			// selected then, reselect it now. This will clear
			// any other selected nodes. e.g. A B C D are selected
			// the user clicks on B, now A C & D are no longer selected.
			try
			{
				// Check to see if a node was clicked on 
				TreeNode node = this.GetNodeAt( e.Location );
				if( node != null )
				{
					if( ModifierKeys == Keys.None && m_SelectedNodes.Contains( node ) )
					{
						int leftBound = node.Bounds.X; // -20; // Allow user to click on image
						int rightBound = node.Bounds.Right + 10; // Give a little extra room
						if( e.Location.X > leftBound && e.Location.X < rightBound )
						{

							SelectNode( node );
						}
					}
				}

				base.OnMouseUp( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnItemDrag( ItemDragEventArgs e )
		{
			// If the user drags a node and the node being dragged is NOT
			// selected, then clear the active selection, select the
			// node being dragged and drag it. Otherwise if the node being
			// dragged is selected, drag the entire selection.
			try
			{
				TreeNode node = e.Item as TreeNode;

				if( node != null )
				{
					if( !m_SelectedNodes.Contains( node ) )
					{
						SelectSingleNode( node );
						ToggleNode( node, true );
					}
				}

				base.OnItemDrag( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnBeforeSelect( TreeViewCancelEventArgs e )
		{
			// Never allow base.SelectedNode to be set!
			try
			{
				base.SelectedNode = null;
				e.Cancel = true;

				base.OnBeforeSelect( e );
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnAfterSelect( TreeViewEventArgs e )
		{
			// Never allow base.SelectedNode to be set!
			try
			{
				base.OnAfterSelect( e );
				base.SelectedNode = null;
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
		}

		protected override void OnKeyDown( KeyEventArgs e )
		{
			// Handle all possible key strokes for the control.
			// including navigation, selection, etc.

			base.OnKeyDown( e );

			if( e.KeyCode == Keys.ShiftKey ) return;

			//this.BeginUpdate();
			bool bShift = ( ModifierKeys == Keys.Shift );

			try
			{
				// Nothing is selected in the tree, this isn't a good state
				// select the top node
				if( m_SelectedNode == null && this.TopNode != null )
				{
					ToggleNode( this.TopNode, true );
				}
			}
			catch( Exception ex )
			{
				HandleException( ex );
			}
			finally
			{
				this.EndUpdate();
			}
		}

		#endregion

		#region Helper Methods

		private void SelectNode( TreeNode node )
        {
            if (node == null || node.Parent == null)
            {
                node.BackColor = Theme.MAIN_TREEVIEW_BACKGROUND;
                SelectedNodes = null;
                return;
            }
            try
			{
				this.BeginUpdate();

				if( m_SelectedNode == null || ModifierKeys == Keys.Control )
				{
					// Ctrl+Click selects an unselected node, or unselects a selected node.
					bool bIsSelected = m_SelectedNodes.Contains( node );
					ToggleNode( node, !bIsSelected );
				}
				else if( ModifierKeys == Keys.Shift )
				{
					// Shift+Click selects nodes between the selected node and here.
					TreeNode ndStart = m_SelectedNode;
					TreeNode ndEnd = node;

					if( ndStart.Parent == ndEnd.Parent )
					{
						// Selected node and clicked node have same parent, easy case.
						if( ndStart.Index < ndEnd.Index )
						{							
							// If the selected node is beneath the clicked node walk down
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.NextVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
						else if( ndStart.Index == ndEnd.Index )
						{
							// Clicked same node, do nothing
						}
						else
						{
							// If the selected node is above the clicked node walk up
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.PrevVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
					}
					else
					{
						// Selected node and clicked node have same parent, hard case.
						// We need to find a common parent to determine if we need
						// to walk down selecting, or walk up selecting.

						TreeNode ndStartP = ndStart;
						TreeNode ndEndP = ndEnd;
						int startDepth = Math.Min( ndStartP.Level, ndEndP.Level );

						// Bring lower node up to common depth
						while( ndStartP.Level > startDepth )
						{
							ndStartP = ndStartP.Parent;
						}

						// Bring lower node up to common depth
						while( ndEndP.Level > startDepth )
						{
							ndEndP = ndEndP.Parent;
						}

						// Walk up the tree until we find the common parent
						while( ndStartP.Parent != ndEndP.Parent )
						{
							ndStartP = ndStartP.Parent;
							ndEndP = ndEndP.Parent;
						}

						// Select the node
						if( ndStartP.Index < ndEndP.Index )
						{
							// If the selected node is beneath the clicked node walk down
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.NextVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
						else if( ndStartP.Index == ndEndP.Index )
						{
							if( ndStart.Level < ndEnd.Level )
							{
								while( ndStart != ndEnd )
								{
									ndStart = ndStart.NextVisibleNode;
									if( ndStart == null ) break;
									ToggleNode( ndStart, true );
								}
							}
							else
							{
								while( ndStart != ndEnd )
								{
									ndStart = ndStart.PrevVisibleNode;
									if( ndStart == null ) break;
									ToggleNode( ndStart, true );
								}
							}
						}
						else
						{
							// If the selected node is above the clicked node walk up
							// selecting each Visible node until we reach the end.
							while( ndStart != ndEnd )
							{
								ndStart = ndStart.PrevVisibleNode;
								if( ndStart == null ) break;
								ToggleNode( ndStart, true );
							}
						}
					}
				}
				else
				{
					// Just clicked a node, select it
					SelectSingleNode( node );
				}

				OnAfterSelect( new TreeViewEventArgs( m_SelectedNode ) );
			}
			finally
			{
				this.EndUpdate();
			}
		}

		private void ClearSelectedNodes()
		{
			try
			{
				foreach( TreeNode node in m_SelectedNodes )
                {
                    if (node == null || node.Parent == null)
                    {
                        return;
                    }
                    node.BackColor = this.BackColor;
					//node.ForeColor = this.ForeColor;
				}
			}
			finally
			{
				m_SelectedNodes.Clear();
				m_SelectedNode = null;
			}
		}

        public void SelectSingleNode( TreeNode node )
        {
            if ( node == null || node.Parent == null)
            {
                return;
			}

			ClearSelectedNodes();
			ToggleNode( node, true );
			node.EnsureVisible();
		}

		public void ToggleNode( TreeNode node, bool bSelectNode )
		{
            if (node == null || node.Parent == null)
            {
                return;
            }
			if( bSelectNode )
			{
				m_SelectedNode = node;
				if( !m_SelectedNodes.Contains( node ) )
				{
					m_SelectedNodes.Add( node );
				}
				node.BackColor = Theme.MAIN_TREEVIEW_HIGHTLIGHT;

				//node.ForeColor = SystemColors.HighlightText;
			}
			else
			{
				m_SelectedNodes.Remove( node );
				node.BackColor = this.BackColor;
				//node.ForeColor = this.ForeColor;
			}
		}

		private void HandleException( Exception ex )
		{
			// Perform some error handling here.
			// We don't want to bubble errors to the CLR. 
			MessageBox.Show(ex.Message , "MultiselectTreeView Error");
		}

        #endregion
    }
}
