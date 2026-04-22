
using UnityEngine;

namespace DTT.Utils.Extensions.Demo
{
    public class StringBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            /* Use string extension methods to modify text for display. */
            _ = "MyString".AddSpacesBeforeCapitals(); //: My String
            _ = "MY_CONSTANT".FromAllCapsToReadableFormat(); //: My Constant
            _ = "My Readable".FromReadableFormatToAllCaps(); //: MY_READABLE

            /* Use string extensions to strip html tags from your text. */
            _ = "<p>Paragraph text</p>".StripHtmlTags(); //: Paragraph text
        }

        private void OnGUI()
        {
            /* Use string extensions to truncate your string after a certain length and at a character to indicate its end. */
            _ = "This text is to long".Ellipsis(5, GUI.skin.font); //: This text is...
        }
    }
}
