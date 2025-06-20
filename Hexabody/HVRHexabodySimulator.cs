using HexabodyVR.PlayerController;
using UnityEngine;

namespace HurricaneVRExtensions.Simulator
{
    public class HVRHexabodySimulator : HVRBodySimulator
    {
        [Header("Hexabody Rig dependencies (Don't assign if 'autoResolveDependencies' is enabled)")]
        [SerializeField] private HVRHexaBodyInputs _hexabodyInputs;
        [SerializeField] private HexaBodyPlayer4 _hexabodyPlayer4;

        protected override void Start()
        {
            if (!ResolveDependencies())
            {
                enabled = false;
                return;
            }

            _hexabodyInputs.KeyboardDebug = true;
        }

        protected override void TurnCamera()
        {
            float rotationAngleY = MouseDelta.y * turnSpeed;
            _hexabodyPlayer4.Camera.transform.RotateAround(_hexabodyPlayer4.Camera.transform.position, _hexabodyPlayer4.Camera.transform.right, -rotationAngleY);
        }

        protected override void TurnRig()
        {
            float rotationAngleX = MouseDelta.x * turnSpeed;
            _hexabodyPlayer4.Pelvis.transform.RotateAround(_hexabodyPlayer4.Pelvis.transform.position, Vector3.up, rotationAngleX);
        }

        #region Initialization
        protected override bool ResolveDependencies()
        {
            if (!autoResolveDependencies)
                return true;

            if (Rig == null)
            {
                Debug.LogError("HVRHexabodySimulator error: Assign the �rig� component in the editor to continue.", gameObject);
                return false;
            }

            _hexabodyPlayer4 = Rig.GetComponentInChildren<HexaBodyPlayer4>();
            _hexabodyInputs = Rig.GetComponentInChildren<HVRHexaBodyInputs>();

            if (_hexabodyPlayer4 == null)
            {
                Debug.Log(DependencyError("HexaBodyPlayer4"));
                return false;
            }

            if (_hexabodyInputs == null)
            {
                Debug.Log(DependencyError("HVRHexaBodyInputs"));
                return false;
            }

            return true;
        }

        private string DependencyError(string component)
        {
            return string.Format("HVRHexabodySimulator AutoResolveDependencies Error: No {0} component found. Make sure Rig parameter is assigned with Hurricane/Hexabody rig root object.", component);
        }
        #endregion

    }
}

