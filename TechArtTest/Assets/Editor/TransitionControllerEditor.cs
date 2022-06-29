using UnityEditor;
using UnityEngine;

namespace Exam.Technical
{

	[CustomEditor(typeof(TransitionController), true)]
	public class TransitionControllerEditor : Editor
	{
		private SerializedProperty _triggerShow;
		private SerializedProperty _triggerHide;
		private SerializedProperty _triggerClick;

		private SerializedProperty _showClass;
		private SerializedProperty _hideClass;
		private SerializedProperty _clickClass;

		private SerializedProperty _isHide;
		private SerializedProperty _isShow;

		private void OnEnable()
		{
			_triggerShow = serializedObject.FindProperty("triggerShow");
			_triggerHide = serializedObject.FindProperty("triggerHide");
			_triggerClick = serializedObject.FindProperty("triggerClick");


			_showClass = serializedObject.FindProperty("showAnimation");
			_hideClass = serializedObject.FindProperty("hideAnimation");
			_clickClass = serializedObject.FindProperty("clickAnimation");

			_isHide = serializedObject.FindProperty("isHide");
			_isShow = serializedObject.FindProperty("isShow");


		}
		/// <summary>
		/// Draw Curve Popup
		/// </summary>
		public static void DrawCurve(Rect position, SerializedProperty property)
		{
			const float BUTTON_SIZE = 14;
			Rect rField = new Rect(position.x, position.y - 1, position.width - BUTTON_SIZE + 1, position.height + 1);
			EditorGUI.PropertyField(rField, property, GUIContent.none);

			Rect rButton = new Rect(rField.x + rField.width, position.y, BUTTON_SIZE, position.height);
			if (GUI.Button(rButton, EditorGUIUtility.FindTexture("icon dropdown"), EditorStyles.label))
			{
				CurvePresetPopup.Dropdown(rField, "EasingCurves", curve =>
				{
					property.animationCurveValue = curve;
					property.serializedObject.ApplyModifiedProperties();
				});
			}
		}
		public void DrawAnimationType(SerializedProperty animationtype, string label)
        {
			var ischangepos = animationtype.FindPropertyRelative("isEnabled");
			EditorGUILayout.PropertyField(ischangepos, new GUIContent(label));
			if (ischangepos.boolValue == true)
			{
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUI.indentLevel++;

					var tarPos = animationtype.FindPropertyRelative("tarVal");
					EditorGUILayout.PropertyField(tarPos, new GUIContent("Value"));

					var curvePos = animationtype.FindPropertyRelative("curve");
					DrawCurve(EditorGUILayout.GetControlRect(false, 24, GUILayout.MaxWidth(128 + 12)), curvePos);

					EditorGUI.indentLevel--;

				}
				EditorGUILayout.Space(2);
			}
		}

		public void DrawAnimationCategory(SerializedProperty animationcategory)
		{


			EditorGUI.indentLevel++;

			var position = animationcategory.FindPropertyRelative("position");
			DrawAnimationType(position, "Change Relative Position");

			var rotation = animationcategory.FindPropertyRelative("rotation");
			DrawAnimationType(rotation, "Change Rotation");

			var scale = animationcategory.FindPropertyRelative("scale");
			DrawAnimationType(scale, "Change Scale");

			var dur = animationcategory.FindPropertyRelative("duration");
			EditorGUILayout.PropertyField(dur);

			EditorGUI.indentLevel--;
			EditorGUILayout.Space(10);
			Rect rect = EditorGUILayout.GetControlRect(false, 1);
			EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
			EditorGUILayout.Space(10);


		}
		public override void OnInspectorGUI()
		{
			// Animation editor on show
			var tr = target as TransitionController;
			EditorGUILayout.PropertyField(_isShow, new GUIContent("Show on Start"));
			_isHide.boolValue = !_isShow.boolValue;

			EditorGUILayout.PropertyField(_triggerShow, new GUIContent("Animate on Show"));
			if (_triggerShow.boolValue == true)
			{
				DrawAnimationCategory(_showClass);
				using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
				{
					EditorGUI.BeginDisabledGroup(!Application.isPlaying);
					if (GUILayout.Button("Show", EditorStyles.toolbarButton))
					{
						tr.Show();
					}
					EditorGUI.EndDisabledGroup();
				}
			}

			// Animation editor on hide
			EditorGUILayout.PropertyField(_triggerHide, new GUIContent("Animate on Hide"));
			if (_triggerHide.boolValue == true)
			{
				DrawAnimationCategory(_hideClass);
				using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
				{

					EditorGUI.BeginDisabledGroup(!Application.isPlaying);
					if (GUILayout.Button("Hide", EditorStyles.toolbarButton))
					{
						tr.Hide();
					}
					EditorGUI.EndDisabledGroup();
				}
			}

			// Animation editor for clicks
			EditorGUILayout.PropertyField(_triggerClick, new GUIContent("Animate on Click"));
			if (_triggerClick.boolValue == true)
				DrawAnimationCategory(_clickClass);
			serializedObject.ApplyModifiedProperties();

		}
	}
}