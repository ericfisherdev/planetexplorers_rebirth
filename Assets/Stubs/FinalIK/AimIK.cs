// Stub: RootMotion.FinalIK.AimIK
// 71 references in game code. Provides AimIK MonoBehaviour with IKSolverAim solver property.

using UnityEngine;

namespace RootMotion.FinalIK
{
    /// <summary>
    /// Aim IK component. Game code accesses .solver.IKPositionWeight, .solver.IKPosition,
    /// .solver.axis, .solver.target, .solver.Update(), .solver.SetIKPositionWeight(), etc.
    /// </summary>
    public class AimIK : IK
    {
        private IKSolverAim _aimSolver = new IKSolverAim();

        public new IKSolverAim solver
        {
            get { return _aimSolver; }
        }

        public override IKSolver GetIKSolver()
        {
            return _aimSolver;
        }
    }
}
