using UnityEngine;
using UnityEditor;

namespace GameField.Editor
{
	[CustomPropertyDrawer(typeof(GamaFieldData))]
	public class CustPropertyDrawer : PropertyDrawer 
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label.text = "Level entites position";
			EditorGUI.PrefixLabel(position, label);
			var newPosition = position;
			var data = property.FindPropertyRelative("Rows");

			for (var j = 0; j < 10; j++)
			{
				var row = data.GetArrayElementAtIndex(j).FindPropertyRelative("RowBlocks");
				newPosition.height = 20f;

				row.arraySize = 7;

				newPosition.width = position.width / 7;
				for (var i = 0; i < 7; i++)
				{
					var positionField = newPosition;

					positionField.y += 40f;
					EditorGUI.PropertyField(positionField, row.GetArrayElementAtIndex(i).FindPropertyRelative("Type"), GUIContent.none);

					positionField.y += 20f;
					EditorGUI.PropertyField(positionField, row.GetArrayElementAtIndex(i).FindPropertyRelative("AmountEntities"), GUIContent.none);

					newPosition.x += newPosition.width;
				}

				newPosition.x = position.x;
				newPosition.y += 60f;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 11 * 60f;
		}
	}
}

