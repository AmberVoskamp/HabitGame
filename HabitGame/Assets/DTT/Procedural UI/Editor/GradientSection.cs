using UnityEditor;
using UnityEngine.Events;

namespace DTT.UI.ProceduralUI.Editor
{
    /// <summary>
    /// Displays the section in the inspector for <see cref="GradientEffect"/> where the user 
    /// can select what type of gradient he/she wants to use.
    /// </summary>
    public class GradientSection : Section<GradientEffect>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string HeaderName => "Gradient";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override bool OpenFoldoutOnEnter => true;

        /// <summary>
        /// The batching property of <see cref="GradientEffect"/>.
        /// </summary>
        private readonly SerializedProperty _batching;

        /// <summary>
        /// The type property of <see cref="GradientEffect"/>.
        /// </summary>
        private readonly SerializedProperty _type;

        /// <summary>
        /// The gradient property of <see cref="GradientEffect"/>.
        /// </summary>
        private readonly SerializedProperty _gradient;

        /// <summary>
        /// The offset property of <see cref="GradientEffect"/>.
        /// </summary>
        private readonly SerializedProperty _offset;

        /// <summary>
        /// The rotation property of <see cref="GradientEffect"/>.
        /// </summary>
        private readonly SerializedProperty _rotation;

        /// <summary>
        /// The scale property of <see cref="GradientEffect"/>.
        /// </summary>
        private readonly SerializedProperty _scale;

        /// <summary>
        /// Creates a new gradient section.
        /// </summary>
        /// <param name="GradientEffect">
        /// The Gradient instance to apply this to.
        /// </param>
        /// <param name="repaint">
        /// When called should repaint the inspector.
        /// </param>
        /// <param name="type">
        /// The type property of <see cref="GradientEffect"/>.
        /// </param>
        /// <param name="gradient">
        /// The gradient property of <see cref="GradientEffect"/>.
        /// </param>
        /// <param name="offset">
        /// The offset property of <see cref="GradientEffect"/>.
        /// </param>
        /// <param name="rotation">
        /// The rotation of <see cref="GradientEffect"/>.
        /// </param>
        /// <param name="scale">
        /// The scale of <see cref="GradientEffect"/>.
        /// </param>
        public GradientSection(
            GradientEffect GradientEffect,
            UnityAction repaint,
            GradientEffectSerializedProperties properties
        ) : base(GradientEffect, repaint)
        {
            _type = properties.type;
            _gradient = properties.gradient;
            _offset = properties.offset;
            _rotation = properties.rotation;
            _scale = properties.scale;
            _batching = properties.batching;
        }

        /// <summary>
        /// Draws the toolbar where the user can select their option, and draws the selected option.
        /// </summary>
        protected override void DrawSection()
        {
            _ = EditorGUILayout.PropertyField(_batching);
            _ = EditorGUILayout.PropertyField(_type);
            _ = EditorGUILayout.PropertyField(_gradient);
            _ = EditorGUILayout.PropertyField(_offset);
            _ = EditorGUILayout.PropertyField(_rotation);
            _ = EditorGUILayout.PropertyField(_scale);
        }
    }
}