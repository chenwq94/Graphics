using System;
using UnityEngine;

namespace UnityEditor.VFX
{
    [VFXInfo(category = "Math")]
    class VFXOperatorFitClamped : VFXOperatorFloatUnifiedWithVariadicOutput
    {
        public class InputProperties
        {
            [Tooltip("The value to be remapped into the new range.")]
            public FloatN input = new FloatN(0.5f);
            [Tooltip("The start of the old range.")]
            public FloatN oldRangeMin = new FloatN(0.0f);
            [Tooltip("The end of the old range.")]
            public FloatN oldRangeMax = new FloatN(1.0f);
            [Tooltip("The start of the new range.")]
            public FloatN newRangeMin = new FloatN(5.0f);
            [Tooltip("The end of the new range.")]
            public FloatN newRangeMax = new FloatN(10.0f);
        }

        override public string name { get { return "Fit (Clamped)"; } }

        protected override VFXExpression[] BuildExpression(VFXExpression[] inputExpression)
        {
            VFXExpression clamped = VFXOperatorUtility.Clamp(inputExpression[0], inputExpression[1], inputExpression[2]);
            return new[] { VFXOperatorUtility.Fit(clamped, inputExpression[1], inputExpression[2], inputExpression[3], inputExpression[4]) };
        }
    }
}
