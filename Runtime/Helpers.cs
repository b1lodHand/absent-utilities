using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace com.absence.utilities
{
    /// <summary>
    /// Holds some handy functions.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Splits input string by capital letters and returns all seperated parts.
        /// </summary>
        public static IEnumerable<string> SplitCamelCaseIndividual(string input)
        {
            return Regex.Split(input, @"([A-Z]?[a-z]+)").Where(str => !string.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Splits input string by capital letters and returns all parts combined with seperator.
        /// </summary>
        public static string SplitCamelCase(string input, string seperator)
        {
            return String.Join(seperator, SplitCamelCaseIndividual(input));
        }

        const float K_SPACING = 5f;
        const float K_PADDING = 5f;
        /// <summary>
        /// Splits input rect by specified parameters.
        /// </summary>
        public static Rect[] SliceRectHorizontally(Rect rect, int pieceCount, float overrideSpacing = K_SPACING, float overridePadding = K_PADDING, params float[] pieceSizeCoefficients)
        {
            if (pieceCount <= 0) return null;

            if(pieceSizeCoefficients == null)
            {
                pieceSizeCoefficients = new float[pieceCount];
                for (int i = 0; i < pieceCount; i++)
                {
                    pieceSizeCoefficients[i] = 1f;
                }
            }

            Rect[] result = new Rect[pieceCount];

            float totalWidth = rect.width - ((2 * overridePadding) + ((pieceCount - 1) * overrideSpacing));
            float generalCoefficient = totalWidth / pieceSizeCoefficients.Sum();

            float horizontalPointer = rect.x + overridePadding;
            for (int i = 0; i < pieceCount; i++)
            {
                float currentSize = generalCoefficient * pieceSizeCoefficients[i];

                Rect current = new Rect(horizontalPointer, rect.y, currentSize, rect.height);
                result[i] = current;

                horizontalPointer += currentSize;
                horizontalPointer += overrideSpacing;
            }

            return result;
        }
    }

}
