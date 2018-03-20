using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dialog
{
    public class Dialog
    {
        public System.Collections.Generic.List<Text> TextsList;

        public int CharacterID
        {
            get => default(int);
            set
            {
            }
        }

        public int SceneID
        {
            get => default(int);
            set
            {
            }
        }
    }

    public class Text
    {
        public string Value
        {
            get => default(int);
            set
            {
            }
        }

        public int ChoiceID
        {
            get => default(int);
            set
            {
            }
        }

        public int ID
        {
            get => default(int);
            set
            {
            }
        }
    }
}