using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utilities.SaveSystem
{
    /// <summary>
    /// Handles saving and retrieving custom classes to/from a file.
    /// </summary>
    public static class SaveSystem
    {
        public const string EXTENSION = ".ffg";

        /// <summary>
        /// Retrieves the saved data based on the file name.
        /// </summary>
        /// <param name="fileName">The save file name.</param>
        /// <returns>
        /// The retrieved data as <see cref="object"/>.
        /// <see cref="null"/> if the file has not been found.
        /// </returns>
        public static object LoadData(string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName + EXTENSION;

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                object data = formatter.Deserialize(stream);

                stream.Close();

                return data;
            }
            else
                return null;
        }

        /// <summary>
        /// Saves the class to a file.
        /// </summary>
        /// <param name="dataScript">The class to be saved.</param>
        /// <param name="fileName">The file name where the data will be saved.</param>
        public static void SaveData(object dataScript, string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName + EXTENSION;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            object data = dataScript;

            formatter.Serialize(stream, data);
            stream.Close();
        }

        /// <summary>
        /// Deletes the saved data if exists.
        /// </summary>
        /// <param name="fileName">The file name to be deleted.</param>
        public static void DeleteData(string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".def";

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
