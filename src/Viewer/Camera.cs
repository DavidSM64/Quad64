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
        ORBIT = 1,
        LOOK_DIRECTION = 2
    }

    enum LookDirection
    {
        TOP, BOTTOM,
        LEFT, RIGHT,
        FRONT, BACK
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

        private LookDirection currentLookDirection;
        private Vector3[] lookPositions = new Vector3[]
        {
            new Vector3(0, 12500, 0), // top
            new Vector3(0, -12500, 0), // bottom
            new Vector3(-12500, 0, 0), // left
            new Vector3(12500, 0, 0), // right
            new Vector3(0, 0, 12500), // front
            new Vector3(0, 0, -12500)  // back
        };

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

        public void setCameraMode_LookDirection(LookDirection dir, ref Matrix4 cameraMatrix)
        {
            camMode = CameraMode.LOOK_DIRECTION;
            currentLookDirection = dir;
            switch (currentLookDirection)
            {
                case LookDirection.TOP:
                    pos = lookPositions[(int)LookDirection.TOP];
                    lookat = new Vector3(pos.X, -25000, pos.Z - 1);
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    setRotationFromLookAt();
                    break;
                case LookDirection.BOTTOM:
                    pos = lookPositions[(int)LookDirection.BOTTOM];
                    lookat = new Vector3(pos.X, 25000, pos.Z + 1);
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    setRotationFromLookAt();
                    break;
                case LookDirection.LEFT:
                    pos = lookPositions[(int)LookDirection.LEFT];
                    lookat = new Vector3(25000, pos.Y, pos.Z);
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    setRotationFromLookAt();
                    break;
                case LookDirection.RIGHT:
                    pos = lookPositions[(int)LookDirection.RIGHT];
                    lookat = new Vector3(-25000, pos.Y, pos.Z);
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    setRotationFromLookAt();
                    break;
                case LookDirection.FRONT:
                    pos = lookPositions[(int)LookDirection.FRONT];
                    lookat = new Vector3(pos.X, pos.Y, -25000);
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    setRotationFromLookAt();
                    break;
                case LookDirection.BACK:
                    pos = lookPositions[(int)LookDirection.BACK];
                    lookat = new Vector3(pos.X, pos.Y, 25000);
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    setRotationFromLookAt();
                    break;
            }
        }

        public void updateCameraMatrixWithMouse(int mouseX, int mouseY, ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT && Globals.item_selected > -1)
                updateCameraMatrixWithMouse_ORBIT(mouseX, mouseY, ref cameraMatrix);
            else if (camMode == CameraMode.LOOK_DIRECTION)
                updateCameraMatrixWithMouse_LOOK(pos, mouseX, mouseY, ref cameraMatrix);
            else
                updateCameraMatrixWithMouse_FLY(mouseX, mouseY, ref cameraMatrix);
        }

        public void updateCameraOffsetWithMouse(Vector3 orgPos, int mouseX, int mouseY, int w, int h, ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT && Globals.item_selected > -1)
                updateCameraOffsetWithMouse_ORBIT(mouseX, mouseY, ref cameraMatrix);
            else if (camMode == CameraMode.LOOK_DIRECTION)
                updateCameraMatrixWithMouse_LOOK(pos, mouseX, mouseY, ref cameraMatrix);
            else
                updateCameraOffsetWithMouse_FLY(orgPos, mouseX, mouseY, w, h, ref cameraMatrix);
        }

        public void updateCameraMatrixWithScrollWheel(int amt, ref Matrix4 cameraMatrix)
        {
            if (camMode == CameraMode.ORBIT && Globals.item_selected > -1)
                updateCameraMatrixWithScrollWheel_ORBIT(amt, ref cameraMatrix);
            else if (camMode == CameraMode.LOOK_DIRECTION)
                updateCameraMatrixWithScrollWheel_LOOK(amt, ref cameraMatrix);
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

        private void updateCameraMatrixWithScrollWheel_LOOK(int amt, ref Matrix4 cameraMatrix)
        {
            offsetCam(amt, amt, amt);
            orientateCam(CamAngleX, CamAngleY);
            switch (currentLookDirection)
            {
                case LookDirection.TOP:
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z - 1, 0, 1, 0);
                    break;
                case LookDirection.BOTTOM:
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z + 1, 0, 1, 0);
                    break;
                default:
                    cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, lookat.X, lookat.Y, lookat.Z, 0, 1, 0);
                    break;
            }
        }

        private void updateCameraMatrixWithMouse_LOOK(Vector3 orgPos, int mouseX, int mouseY, ref Matrix4 cameraMatrix)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            int MousePosX = mouseX - lastMouseX;
            int MousePosY = mouseY - lastMouseY;

            double pitch = CamAngleY - (Math.PI / 2);
            double yaw = CamAngleX - (Math.PI / 2);
            float CamLX = (float)Math.Sin(yaw);
            float CamLY = (float)Math.Cos(pitch);
            float CamLZ = (float)-Math.Cos(yaw);

            float m = 8f;

            switch (currentLookDirection)
            {
            case LookDirection.TOP:
                pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * m) -
                    ((MousePosY * Globals.camSpeedMultiplier) * (CamLZ) * m);
                pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * m) -
                    ((MousePosY * Globals.camSpeedMultiplier) * (CamLX) * m);
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, pos.X, pos.Y - 1000, pos.Z - 1, 0, 1, 0);
                lookPositions[(int)currentLookDirection] = pos;
                break;
            case LookDirection.BOTTOM:
                pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * m) +
                    ((MousePosY * Globals.camSpeedMultiplier) * (CamLZ) * m);
                pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * m) +
                    ((MousePosY * Globals.camSpeedMultiplier) * (CamLX) * m);
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, pos.X, pos.Y + 1000, pos.Z + 1, 0, 1, 0);
                lookPositions[(int)currentLookDirection] = pos;
                break;
            case LookDirection.LEFT:
                pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * m);
                pos.Y = orgPos.Y - ((MousePosY * Globals.camSpeedMultiplier) * (-1f) * m);
                pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * m);
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, pos.X + 12500, pos.Y, pos.Z, 0, 1, 0);
                    lookPositions[(int)currentLookDirection] = pos;
                    break;
           case LookDirection.RIGHT:
                pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * m);
                pos.Y = orgPos.Y - ((MousePosY * Globals.camSpeedMultiplier) * (-1f) * m);
                pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * m);
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, pos.X - 12500, pos.Y, pos.Z, 0, 1, 0);
                lookPositions[(int)currentLookDirection] = pos;
                break;
            case LookDirection.FRONT:
                pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * m);
                pos.Y = orgPos.Y - ((MousePosY * Globals.camSpeedMultiplier) * (-1f) * m);
                pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * m);
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, pos.X, pos.Y, pos.Z - 12500, 0, 1, 0);
                lookPositions[(int)currentLookDirection] = pos;
                break;
            case LookDirection.BACK:
                pos.X = orgPos.X - ((MousePosX * Globals.camSpeedMultiplier) * (CamLX) * m);
                pos.Y = orgPos.Y - ((MousePosY * Globals.camSpeedMultiplier) * (-1f) * m);
                pos.Z = orgPos.Z - ((MousePosX * Globals.camSpeedMultiplier) * (CamLZ) * m);
                cameraMatrix = Matrix4.LookAt(pos.X, pos.Y, pos.Z, pos.X, pos.Y, pos.Z + 12500, 0, 1, 0);
                lookPositions[(int)currentLookDirection] = pos;
                break;
            }

            lastMouseX = mouseX;
            lastMouseY = mouseY;
            //Console.WriteLine("CamAngleX = " + CamAngleX + ", CamAngleY = " + CamAngleY);
            //setRotationFromLookAt();
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

        public void updateCameraOffsetDirectly(int horz_amount, int vert_amount, ref Matrix4 cameraMatrix)
        {
            //Console.WriteLine(MousePosX+","+ MousePosY);
            double pitch = CamAngleY - (Math.PI / 2);
            double yaw = CamAngleX - (Math.PI / 2);
            float CamLX = (float)Math.Sin(yaw);
            // float CamLY = (float)Math.Cos(pitch);
            float CamLZ = (float)-Math.Cos(yaw);
            pos.X += ((horz_amount*Globals.camSpeedMultiplier) * (CamLX));
            pos.Y += ((vert_amount*Globals.camSpeedMultiplier) * (-1f));
            pos.Z += ((horz_amount*Globals.camSpeedMultiplier) * (CamLZ));

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
