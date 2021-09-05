﻿/*
IniFile class 
by Todd Davis (toddhd@hotmail.com)
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
to permit persons to whom the Software is furnished to do so, subject to the following conditions:

This permission notice shall be included in all copies or substantial portions 
of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.

Converted to C# and stylished by Tomás A. Cardoner, september 2021
*/

using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace CardonerSistemas.Configuration
{

    internal class IniFile
    {
        private ArrayList sections = new ArrayList();
        private const string CommentString = ";";

        private enum LineContents : byte
        {
            Section,
            Key,
            Comment,
            Blank,
            Unknown
        }

        internal bool Read(string filename, bool createIfNotExist = true)
        {
            // Verify that the file exists
            if (!File.Exists(filename))
            {
                // If it does not exist, check to see if we should create it
                if (createIfNotExist)
                {
                    try
                    {
                        // Create it
                        FileStream fs = File.Create(filename);
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: Cannot create file {filename}\n\n{ex.ToString()}", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show($"Error: File {filename} does not exist. Cannot create IniFile.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Clear the arraylist
            sections.Clear();

            try
            {
                // Declare a streamreader
                StreamReader sr = new StreamReader(filename);
                // Flag to track what section we are currently in
                string currentSection = string.Empty;
                // Read in the first line
                string thisLine = sr.ReadLine();

                do
                {
                    // Evalue the contents of the line
                    switch (Eval(thisLine))
                    {
                        case LineContents.Section:
                            // Add the section to the sections arraylist
                            AddSection(RemoveBrackets(RemoveComment(thisLine)), IsCommented(thisLine));
                            // Make this the current section, so we know where to keys to
                            currentSection = RemoveBrackets(RemoveComment(thisLine));
                            break;
                        case LineContents.Key:
                            AddKey(GetKeyName(thisLine), GetKeyValue(thisLine), currentSection, IsCommented(thisLine));
                            break;
                        case LineContents.Comment:
                            // AddComment(thisLine, currentSection);
                            break;
                        case LineContents.Blank:
                            // TODO: Should we create a blank object to handle blanks?
                            break;
                        case LineContents.Unknown:
                        default:
                            // We hit something unknown - ignore it
                            break;
                    }

                    // Get the next line
                    thisLine = sr.ReadLine();

                    // continue until the end of the file
                } while (thisLine != null);

                // close the file
                sr.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private LineContents Eval(string value)
        {
            // Remove any leading/trailing spaces, just in case they exist
            value = value.Trim();

            // If the value is blank, then it is a blank line
            if (string.IsNullOrWhiteSpace(value))
            {
                return LineContents.Blank;
            }

            // If the value is surrounded by brackets, then it is a section
            if (RemoveComment(value).StartsWith("[") && RemoveComment(value).EndsWith("]"))
            {
                return LineContents.Section;
            }

            // If the value contains an equals sign (=), then it is a value. This test can be fooled by 
            // comment with an equals sign in it, but it is the best test we have. We test for this before
            // testing for a comment in case the key is commented out. It is still a key.
            if (RemoveComment(value).Contains("="))
            {
                return LineContents.Key;
            }

            // If the value is preceeded by the comment string, then it is a pure comment
            if (IsCommented(value))
            {
                return LineContents.Comment;
            }

            return LineContents.Unknown;
        }

        private string GetKeyName(string value)
        {
            // If the value is commented out, then remove the comment string so we can get the name
            if (IsCommented(value))
            {
                value = RemoveComment(value);
            }

            // Locate the equals sign
            int equalsPos = value.IndexOf("=");

            // It should be, but just to be safe
            if (equalsPos > 0)
            {
                // Return everything before the equals sign
                return value.Substring(0, equalsPos - 1);
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetKeyValue(string value)
        {
            // Locate the equals sign
            int equalsPos = value.IndexOf("=");

            // It should be, but just to be safe
            if (equalsPos > 0 & equalsPos < value.Length - 1)
            {
                // Return everything after the equals sign
                return value.Substring(equalsPos + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        private bool IsCommented(string value)
        {
            // Return true if the passed value starts with a comment string
            return value.StartsWith(CommentString);
        }

        private string RemoveComment(string value)
        {
            // Return the value with the comment string stripped
            if (IsCommented(value))
            {
                return value.Substring(CommentString.Length);
            }
            else
            {
                return value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adds a key/value to a given section. If the section does not exist, it is created.
        /// </summary>
        /// <param name="KeyName">The name of the key to add. If the key alreadys exists, then no action is taken.</param>
        /// <param name="KeyValue">The value to assign to the new key.</param>
        /// <param name="SectionName">The section to add the new key to. If it does not exist, it is created.</param>
        /// <param name="IsCommented">Optional, defaults to false. Will create the key in commented state.</param>
        /// <param name="InsertBefore">Optional. Will insert the new key prior to the specified key.</param>
        /// <returns></returns>
        /// <remarks>If the section does not exist, it will be created. If the 'IsCommented' option is true, then the newly created section will also be commented. If the 'InsertBefore' option is used, the specified key does not exist, then the new key is simply added to the section. If the section the key is being added to is commented, then the key will be commented as well.
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool AddKey(string keyName, string keyValue, string sectionName, bool isCommented = false, string insertBefore = null)
        {
            // Verify that the section exists
            Section thisSection = GetSection(sectionName);

            if (thisSection == null)
            {
                AddSection(sectionName, isCommented);
            }

            // If the section is commented out, then this key must be too
            if (thisSection.IsCommented)
            {
                isCommented = true;
            }

            // Verify that the key does *not* exist
            if (GetKey(keyName, sectionName) != null)
            {
                return false;
            }

            // Create a new key
            Key thisKey = new Key(keyName, keyValue, isCommented);

            // If no insertbefore is required
            if (insertBefore == null)
            {
                // Then add the new key to the bottom of the section
                thisSection.Add(thisKey);
                return true;
            }
            else
            {
                // Locate the key to insert prior to
                int keyIndex = GetKeyIndex(insertBefore, sectionName);
                // If the key exists
                if (keyIndex > -1)
                {
                    // Then do the insert
                    thisSection.Insert(keyIndex, thisKey);
                    return true;
                }
                else
                {
                    // The key to insert prior to wasn't found, so just add it
                    thisSection.Add(thisKey);
                    // The key to insert prior to was not found
                    return false;
                }
            }
        }

        private int GetKeyIndex(string keyName, string sectionName)
        {
            IEnumerator sectionEnumerator = sections.GetEnumerator();
            while (sectionEnumerator.MoveNext())
            {
                Section currentSection = (Section)sectionEnumerator.Current;
                if (currentSection.Name == sectionName)
                {
                    IEnumerator keyEnumerator = currentSection.GetEnumerator();
                    while (keyEnumerator.MoveNext())
                    {
                        Key currentKey = (Key)keyEnumerator.Current;
                        if (currentKey.Name == keyName)
                        {
                            return currentSection.IndexOf(currentKey);
                        }
                    }
                }
            }

            return -1;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adds a section to the IniFile. If the section already exists, then no action is taken.
        /// </summary>
        /// <param name="SectionName">The name of the section to add.</param>
        /// <param name="IsCommented">Optional, defaults to false. Will add the section in a commented state.</param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public void AddSection(string sectionName, bool isCommented = false)
        {
            if (GetSection(sectionName) == null)
            {
                // Add the section to the sections arraylist
                sections.Add(new Section(sectionName, isCommented));
            }
        }

        private Section GetSection(string sectionName)
        {
            IEnumerator myEnumerator = sections.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Section currentSection = (Section)myEnumerator.Current;
                if (string.Compare(currentSection.Name, sectionName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return currentSection;
                }
            }
            return null;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Return the sections in the IniFile.
        /// </summary>
        /// <returns>Returns an ArrayList of Section objects.</returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal ArrayList GetSections()
        {
            ArrayList listOfSections = new ArrayList();
            IEnumerator myEnumerator = sections.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                listOfSections.Add(myEnumerator.Current);
            }
            return listOfSections;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Returns an arraylist of Key objects in a given Section. Section must exist.
        /// </summary>
        /// <param name="sectionName">The name of the Section to retrieve the keys from.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal ArrayList GetKeys(string sectionName)
        {
            ArrayList listOfKeys = new ArrayList();
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return null;
            }
            IEnumerator keyEnumerator = thisSection.GetEnumerator();
            while (keyEnumerator.MoveNext())
            {
                listOfKeys.Add(keyEnumerator.Current);
            }
            return listOfKeys;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comments or uncomments a given section, including all of the keys contained in the section.
        /// </summary>
        /// <param name="sectionName">The name of the Section to comment.</param>
        /// <param name="commented">true for comment, false for uncomment</param>
        /// <returns></returns>
        /// <remarks>Keys that are already commented will <b>not</b> preserve their comment status if 'UnCommentSection' is used later on.
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// 	[TCardoner] 4/9/2021    Unified to one function to do both operations. Added a third parameter to indicate if comment or uncomment
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool ChangeCommentSection(string sectionName, bool commented)
        {
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return false;
            }
            thisSection.IsCommented = commented;
            IEnumerator myEnumerator = thisSection.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Key currentKey = (Key)myEnumerator.Current;
                currentKey.IsCommented = commented;
            }
            return true;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comments or uncomments a given key in a given section. Both the key and the section must exist. 
        /// </summary>
        /// <param name="keyName">The name of the key to comment.</param>
        /// <param name="sectionName">The name of the section the key is in.</param>
        /// <param name="comment">true for comment, false for uncomment</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// 	[TCardoner] 4/9/2021    Unified to one function to do both operations. Added a third parameter to indicate if comment or uncomment
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool ChangeCommentKey(string keyName, string sectionName, bool comment)
        {
            Key thisKey = GetKey(keyName, sectionName);
            if (thisKey == null)
            {
                return false;
            }
            else
            {
                thisKey.IsCommented = comment;
                return true;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Renames a section. The section must exist.
        /// </summary>
        /// <param name="sectionName">The name of the section to be renamed.</param>
        /// <param name="newSectionName">The new name of the section.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool RenameSection(string sectionName, string newSectionName)
        {
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return false;
            }

            thisSection.Name = newSectionName;
            return true;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Renames a given key key in a given section. Both they key and the section must exist. The value is not altered.
        /// </summary>
        /// <param name="keyName">The name of the key to be renamed.</param>
        /// <param name="sectionName">The name of the section the key exists in.</param>
        /// <param name="newKeyName">The new name of the key.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool RenameKey(string keyName, string sectionName, string newKeyName)
        {
            Key thisKey = GetKey(keyName, sectionName);
            if (thisKey == null)
            {
                return false;
            }

            thisKey.Name = newKeyName;
            return true;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Changes the value of a given key in a given section. Both the key and the section must exist.
        /// </summary>
        /// <param name="keyName">The name of the key whose value should be changed.</param>
        /// <param name="sectionName">The name of the section the key exists in.</param>
        /// <param name="newValue">The new value to assign to the key.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool ChangeValue(string keyName, string sectionName, string newValue)
        {
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return false;
            }

            Key thisKey = GetKey(keyName, sectionName);
            if (thisKey == null)
            {
                return false;
            }

            thisKey.Value = newValue;
            return true;
        }

        internal Key GetKey(string keyName, Section section)
        {
            if (section == null)
            {
                return null;
            }

            IEnumerator myEnumerator = section.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Key currentKey = (Key)myEnumerator.Current;
                if (string.Compare(currentKey.Name, keyName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return currentKey;
                }
            }
            return null;
        }

        internal Key GetKey(string keyName, string sectionName)
        {
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return null;
            }
            else
            {
                return GetKey(keyName, sectionName);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deletes a given section. The section must exist. All the keys in the section will also be deleted.
        /// </summary>
        /// <param name="sectionName">The name of the section to be deleted.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool DeleteSection(string sectionName)
        {
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return false;
            }

            sections.Remove(thisSection);
            return true;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deletes a given key in a given section. Both the key and the section must exist.
        /// </summary>
        /// <param name="keyName">The name of the key to be deleted.</param>
        /// <param name="sectionName">The name of the section the key exists in.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[TDavis]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        internal bool DeleteKey(string keyName, string sectionName)
        {
            Section thisSection = GetSection(sectionName);
            if (thisSection == null)
            {
                return false;
            }

            Key thisKey = GetKey(keyName, sectionName);
            if (thisKey == null)
            {
                return false;
            }

            thisSection.Remove(thisKey);
            return true;
        }

        internal void Save(string fileName)
        {
            if (File.Exists(fileName))
            {
                // Remove the existing file
                File.Delete(fileName);
            }

            // Loop through the arraylist (Content) and write each line to the file
            StreamWriter sw = new StreamWriter(fileName);

            IEnumerator sectionEnumerator = sections.GetEnumerator();
            while (sectionEnumerator.MoveNext())
            {
                Section currentSection = (Section)sectionEnumerator.Current;
                sw.Write(AddBrackets(currentSection.Name) + "\r\n");

                IEnumerator keyEnumerator = currentSection.GetEnumerator();
                while (keyEnumerator.MoveNext())
                {
                    Key currentKey = (Key)keyEnumerator.Current;
                    sw.Write($"{currentKey.Name}={currentKey.Value}\r\n");
                }
            }
            sw.Close();
        }

        private string RemoveBrackets(string value)
        {
            value = value.Trim();
            value = value.TrimStart('[');
            value = value.TrimEnd('[');
            return value;
        }

        private string AddBrackets(string value)
        {
            return "[" + value.Trim() + "]";
        }
    }


    internal class Section : ArrayList
    {
        internal string Name { get; set; }
        internal bool IsCommented { get; set; }

        internal Section(string name, bool isCommented = false)
        {
            Name = name;
            IsCommented = isCommented;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null | this.GetType() != obj.GetType())
            {
                return false;
            }
            Section s = (Section)obj;
            return (this.Name == s.Name && this.IsCommented == s.IsCommented);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    internal class Key
    {
        internal string Name { get; set; }
        internal string Value { get; set; }
        internal bool IsCommented { get; set; }

        internal Key(string name, string value, bool isCommented = false)
        {
            Name = name;
            Value = value;
            IsCommented = isCommented;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null | this.GetType() != obj.GetType())
            {
                return false;
            }
            Key k = (Key)obj;
            return (this.Name == k.Name && this.Value == k.Value && this.IsCommented == k.IsCommented);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}