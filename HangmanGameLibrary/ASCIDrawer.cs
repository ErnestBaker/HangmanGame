using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameLibrary
{
    public class ASCIDrawer
    {
        public static string DrawASCIHangman(int step)
        {
            string[,] hangmantable = new String[7,7];

            switch (step)
            {
                case 1:
                    hangmantable[0,6] = "#";

                    for (int i = 0; i < 7; i++)
                    {
                        for (int y = 0; y < 7; y++)
                        {
                            if (i == 0)
                            {
                                continue;
                            }
                            else if (i < 3)
                            {
                                hangmantable[y,i] = "#";
                            }
                        }
                        hangmantable[6,i] = "#";
                    }
                    goto case 2;
                case 2:

                default:
                    break;
            }

            string textToReturn = "";

            for (int i = 0; i < 7; i++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if( hangmantable[i,y] == null)
                    {
                        textToReturn += " ";
                    }
                    else
                    {
                        textToReturn += hangmantable[i, y];
                    }
                }
                textToReturn += Environment.NewLine;
            }
                

            return textToReturn;
        }
    }
}
