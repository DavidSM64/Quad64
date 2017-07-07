using OpenTK;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quad64
{
    enum CameraMode
    {
        FLY = 0,
        ORBIT = 1
    }

    class Camera
    {
        private readonly float TAU = (float)(Math.PI * 2);

        // Camera mode
        private CameraMode camMode = CameraMode.FLY;
        private Level level;
        private Vector3 pos = new Vector3(-5000f, 3000f, 4000f);
        private Vector3 lookat = new Vector3(0f, 0f, 0f);
        private Vector3 farPoint = new Vector3(0f, 0f, 0f);
        private Vector3 nearPoint = new Vector3(0f, 0f, 0f);
        private int lastMouseX = -1, lastMouseY = -1;
        private float CamAngleX = 0f, CamAngleY = -(float)(Math.PI / 2);
        private bool resetMouse = true;
        //private Matrix4 matrix = Matrix4.Identity;

        public CameraMode CamMode { get { return camMode; } }
        public float Yaw { get { return CamAngleX; } }
        public float Pitch { get { return CamAngleY; } }
        public float Yaw_Degrees { get { return CamAngleX * (180.0f/ 3.1415926535897f); } }
        public float Pitch_Degrees { get { return CamAngleY * (180.0f / 3.1415926535897f); } }

        public Vector3 Position { get { return pos; } set { pos = value; } }
        public Vector3 LookAt { get { return lookat; } set { lookat = value; } }
        public Vector3 NearPoint { get { return nearPoint; } set { nearPoint = value; } }
        public Vector3 FarPoint { get { return farPoint; } set { farPoint = value; } }
        //public Matrix4 CameraMatrix { get { return matrix; } set { matrix = value; } }

        private float orbitDistance = 500.0f;
        private float orbitTheta = 0.0f, orbitPhi = 0.0f;

        public Camera()
        {
            setRotationFromLookAt();
        }

        public void setLevel(Level level)
        {
            this.level = level;
            camMode = CameraMode.FLY;
        }

        private float clampf(float value, float min, float max)
        {
            return (value > max ? max : (value < min ? min : value));
        }

        private void orientateCam(float ang, float ang2)
        {
            float CamLX = (float)Math.Sin(ang) * (float)Math.Sin(-ang2);
            float CamLY = (float)Math.Cos(ang2);
            float CamLZ = (float)-Math.Cos(ang) * (float)Math.Sin(-ang2);

            lookat.X = pos.X + (-CamLX) * 100f;
            lookat.Y = pos.Y + (-CamLY) * 100f;
            lookat.Z = pos.Z + (-CamLZ) * 100f;
        }

        private void offsetCam(int xAmt, int yAmt, int zAmt)
        {
            double pitch = CamAngleY - (Math.PI/2);
            //Console.WriteLine("Math.Sin(pitch) = " + Math.Sin(pitch));
            float CamLX = (float)Math.Sin(CamAngleX) * (float)Math.Cos(-pitch);
            float CamLY = (float)Math.Sin(pitch);
            float CamLZ = (float)-Math.Cos(CamAngleX) * (float)Math.Cos(-pitch);
            pos.X = pos.X + xAmt * (CamLX) * Globals.camSpeedMultiplier;
            pos.Y = pos.Y + yAmt * (CamLY) * Globals.camSpeedMultiplier;
            pos.Z = pos.Z + zAmt * (CamLZ) * Globals.camSpeedMultiplier;
        }

        public void setRotationFromLookAt()
        {
            float x_diff = lookat.X - pos.X;
            float y_diff = lookat.Y - pos.Y;
            float z_diff = lookat.Z - pos.Z;
            float dist = (float)Math.Sqrt(x_diff * x_diff + y_diff * y_diff + z_diff * z_diff);
            if (z_diff == 0) z_diff = 0.001f;
            float nxz_ratio = -x_diff / z_diff;
            if (z_diff < 0)
                CamAngleX = (float)(Math.Atan(nxz_ratio) + Math.PI);
            else
                CamAngleX = (float)(Math.Atan(nxz_ratio));
            CamAngleY = -3.1459f - ((float)(Math.Asin(y_diff/dist)) - 1.57f);
        }

        private Object3D getSelectedObject()
        {
            if (Globals.list_selected == -1 || Globals.item_selected == -1)
                return null;
            switch (Globals.list_selected)
            {
                case 0:
                    return level.getCurrentArea().Objects[Globals.item_selected];
                case 1:
                    return level.getCurrentArea().MacroObjects[Globals.item_selected];
                case 2:
                    return level.getCurrentArea().SpecialObjects[Globals.item_selected];
                default:
                    return null;
            }
        }

        public void resetOrbitToSelectedObject()
        {
            Object3D obj = getSelectedObject();
            if (obj != null)
            {
                orbitTheta = -(obj.yRot * ((float)Math.PI / 180.0f));
                orbitPhi = -0.3f;
                orbitDistance = 1200.0f;
            }
        }

        public void updateOrbitCamera(ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT)
            {
                Object3D obj = getSelectedObject();
                if (obj == null)
                    return;
                pos.X = obj.xPos + (float)(Math.Cos(orbitPhi) * -Math.Sin(orbitTheta) * orbitDistance);
                pos.Y = obj.yPos + (float)(-Math.Sin(orbitPhi) * orbitDistance);
                pos.Z = obj.zPos + (float)(Math.Cos(orbitPhi) * Math.Cos(orbitTheta) * orbitDistance);
                lookat.X = obj.xPos;
                lookat.Y = obj.yPos;
                lookat.Z = obj.zPos;
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                setRotationFromLookAt();
            }
        }

        public bool isOrbitCamera()
        {
            return camMode == CameraMode.ORBIT;
        }

        public void setCameraMode(CameraMode mode, ref Matrix4 cameraMatrix)
        {
            camMode = mode;
            if (isOrbitCamera())
            {
                resetOrbitToSelectedObject();
                updateOrbitCamera(ref cameraMatrix);
            }
        }

        public void updateCameraMatrixWithMouse(int mouseX, int mouseY, ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT && Globals.item_selected > -1)
                updateCameraMatrixWithMouse_ORBIT(mouseX, mouseY, ref cameraMatrix);
            else
                updateCameraMatrixWithMouse_FLY(mouseX, mouseY, ref cameraMatrix);
        }

        public void updateCameraOffsetWithMouse(Vector3 orgPos, int mouseX, int mouseY, int w, int h, ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT && Globals.item_selected > -1)
                updateCameraOffsetWithMouse_ORBIT(mouseX, mouseY, ref cameraMatrix);
            else
                updateCameraOffsetWithMouse_FLY(orgPos, mouseX, mouseY, w, h, ref cameraMatrix);
        }

        public void updateCameraMatrixWithScrollWheel(int amt, ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT && Globals.item_selected > -1)
                updateCameraMatrixWithScrollWheel_ORBIT(amt, ref cameraMatrix);
            else
                updateCameraMatrixWithScrollWheel_FLY(amt, ref cameraMatrix);
        }

        private void updateCameraMatrixWithScrollWheel_FLY(int amt, ref Matrix4 cameraMatrix)
        {
            offsetCam(amt, amt, amt);
            orientateCam(CamAngleX, CamAngleY);
            cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
        }

        public void updateMatrix(ref Matrix4 cameraMatrix)
        {
            cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
        }

        private void updateCameraMatrixWithMouse_FLY(int mouseX, int mouseY, ref Matrix4 cameraMatrix)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            int MousePosX = mouseX - lastMouseX;
            int MousePosY = mouseY - lastMouseY;
            CamAngleX = CamAngleX + (0.01f * MousePosX);
            // This next part isn't neccessary, but it keeps the Yaw rotation value within [0, 2*pi] which makes debugging simpler.
            if (CamAngleX > TAU) CamAngleX -= TAU;
            else if (CamAngleX < 0) CamAngleX += TAU;

            /* Lock pitch rotation within the bounds of [-3.1399.0, -0.0001], otherwise the LookAt function will snap to the 
               opposite side and reverse mouse looking controls.*/
            CamAngleY = clampf((CamAngleY + (0.01f * MousePosY)), -3.1399f, -0.0001f);
            
            lastMouseX = mouseX;
            lastMouseY = mouseY;
            orientateCam(CamAngleX, CamAngleY);
            cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
            //Console.WriteLine("CamAngleX = " + CamAngleX + ", CamAngleY = " + CamAngleY);
            //setRotationFromLookAt();
        }

        private void updateCameraOffsetWithMouse_FLY(Vector3 orgPos, int mouseX, int mouseY, int w, int h, ref Matrix4 cameraMatrix)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            int MousePosX = (-mouseX) + lastMouseX;
            int MousePosY = (-mouseY) + lastMouseY;
            //Console.WriteLine(MousePosX+","+ MousePosY);
            double pitch = CamAngleY - (Math.PI / 2);
            double yaw = CamAngleX - (Math.PI / 2);
            float CamLX = (float)Math.Sin(yaw);
           // float CamLY = (float)Math.Cos(pitch);
            float CamLZ = (float)-Math.Cos(yaw);
            pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * 6f);
            pos.Y = orgPos.Y - ((MousePosY * Globals.camSpeedMultiplier) * (-1f) * 6f);
            pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * 6f);
            
            orientateCam(CamAngleX, CamAngleY);
            cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
        }

        private void updateCameraMatrixWithMouse_ORBIT(int mouseX, int mouseY, ref Matrix4 cameraMatrix)
        {
            updateCameraOffsetWithMouse_ORBIT(mouseX, mouseY, ref cameraMatrix);
        }

        private void updateCameraOffsetWithMouse_ORBIT(int mouseX, int mouseY, ref Matrix4 cameraMatrix)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            int MousePosX = (-mouseX) + lastMouseX;
            int MousePosY = (-mouseY) + lastMouseY;
            orbitTheta += MousePosX * 0.01f * Globals.camSpeedMultiplier;
            orbitPhi -= MousePosY * 0.01f * Globals.camSpeedMultiplier;
            orbitPhi = clampf(orbitPhi, -1.57f, 1.57f);
            updateOrbitCamera(ref cameraMatrix);
            lastMouseX = mouseX;
            lastMouseY = mouseY;
            //Console.WriteLine("ORBIT_THETA: " + orbitTheta + ", ORBIT_PHI: " + orbitPhi);
        }

        private void updateCameraMatrixWithScrollWheel_ORBIT(int amt, ref Matrix4 cameraMatrix)
        {
            orbitDistance -= amt;
            if (orbitDistance < 300.0f)
                orbitDistance = 300.0f;
            updateOrbitCamera(ref cameraMatrix);
        }

        public void resetMouseStuff()
        {
            resetMouse = true;
        }
    }
}
