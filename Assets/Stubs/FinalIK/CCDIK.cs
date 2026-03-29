// Stub: RootMotion.FinalIK.CCDIK
// Used in IKCombat.cs. Provides CCDIK MonoBehaviour with IKSolverCCD solver.

using UnityEngine;

namespace RootMotion.FinalIK
{
    /// <summary>
    /// Cyclic Coordinate Descent IK component. Game code accesses
    /// .solver.SetIKPosition(), .solver.SetIKPositionWeight(), .solver.transform.
    /// </summary>
    public class CCDIK : IK
    {
        private IKSolverCCD _ccdSolver = new IKSolverCCD();

        public new IKSolverCCD solver
        {
            get { return _ccdSolver; }
        }

        public override IKSolver GetIKSolver()
        {
            return _ccdSolver;
        }
    }
}
