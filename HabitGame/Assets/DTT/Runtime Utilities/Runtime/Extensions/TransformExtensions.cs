using System;
using UnityEngine;

namespace DTT.Utils.Extensions
{
    /// <summary>
    /// Provides extension methods for transform instances.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Returns the first child transform. Will return null if the transform has no children.
        /// </summary>
        /// <param name="transform">The transform to get the first child of.</param>
        /// <returns>The first child.</returns>
        public static Transform FirstChild(this Transform transform)
        {
            return transform == null
                ? throw new ArgumentNullException(nameof(transform))
                : transform.childCount == 0 ? null : transform.GetChild(0);
        }

        /// <summary>
        /// Returns the last child transform. Will return null if the transform has no children.
        /// </summary>
        /// <param name="transform">The transform to get the last child of.</param>
        /// <returns>The last child.</returns>
        public static Transform LastChild(this Transform transform)
        {
            return transform == null
                ? throw new ArgumentNullException(nameof(transform))
                : transform.childCount == 0 ? null : transform.GetChild(transform.childCount - 1);
        }
    }
}
