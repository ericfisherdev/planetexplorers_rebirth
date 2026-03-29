// Stub: RootMotion.FinalIK.FullBodyBipedIK
// 15 references in game code. Provides FullBodyBipedIK MonoBehaviour with IKSolverFullBodyBiped solver.

using UnityEngine;

namespace RootMotion.FinalIK
{
    /// <summary>
    /// Full body biped IK component. Game code accesses .solver.iterations,
    /// .solver.leftArmMapping, .solver.rightArmMapping, .solver.IKPositionWeight,
    /// .solver.clampSmoothing, .solver.maxFootRotationAngle, .solver.Update().
    /// </summary>
    public class FullBodyBipedIK : IK
    {
        private IKSolverFullBodyBiped _fbbikSolver = new IKSolverFullBodyBiped();

        public new IKSolverFullBodyBiped solver
        {
            get { return _fbbikSolver; }
        }

        public override IKSolver GetIKSolver()
        {
            return _fbbikSolver;
        }
    }
}
