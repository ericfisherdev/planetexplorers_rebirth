using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Pathea.Operate
{

    public class PERides : Operation_Multiple
    {
        [SerializeField]
        PERide[] Rides;
        public override List<Operation_Single> GetSingles()
        {
            return (Rides == null || Rides.Length == 0) ? null : new List<Operation_Single>(Rides);
        }

        public PERide GetUseable()
        {
            var singles = GetSingles();
            for (int i = 0; i < singles.Count; i++)
            {
                if (singles[i].CanOperate(null))
                    return singles[i] as PERide;
            }
            return null;
        }

        public bool HasRide()
        {
            var singles = GetSingles();
            for (int i = 0; i < singles.Count; i++)
            {
                if (singles[i].CanOperate(null))
                    return true;
            }
            return false;
        }

        public bool HasOperater(IOperator op)
        {
            var singles = GetSingles();
            for (int i = 0; i < singles.Count; i++)
            {
                if (singles[i].ContainsOperator(op))
                    return true;
            }
            return false;
        }

        public PERide GetRideByOperater(Pathea.OperateCmpt op)
        {
            var singles = GetSingles();
            for (int i = 0; i < singles.Count; i++)
            {
                if (singles[i].ContainsOperator(op))
                    return (PERide)singles[i];
            }
            return null;
        }
    }
}
