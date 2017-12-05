using System;
using System.IO;
using System.Xml;

namespace Zhzuo
{
    /// <summary>
    /// FileDirectoryUtility ��,�����������쳣����
    /// </summary>
    public class FileDirectoryUtility
    {
        /// <summary>
        /// ·���ָ��
        /// </summary>
        private const string PATH_SPLIT_CHAR = "\\";

        /// <summary>
        /// ���캯��
        /// </summary>
        private FileDirectoryUtility()
        {
        }

        /// <summary>
        /// ����ָ��Ŀ¼�������ļ�,��������Ŀ¼����Ŀ¼�е��ļ�
        /// </summary>
        /// <param name="sourceDir">ԭʼĿ¼</param>
        /// <param name="targetDir">Ŀ��Ŀ¼</param>
        /// <param name="overWrite">���Ϊtrue,��ʾ����ͬ���ļ�,���򲻸���</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite)
        {
            CopyFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// ����ָ��Ŀ¼�������ļ�
        /// </summary>
        /// <param name="sourceDir">ԭʼĿ¼</param>
        /// <param name="targetDir">Ŀ��Ŀ¼</param>
        /// <param name="overWrite">���Ϊtrue,����ͬ���ļ�,���򲻸���</param>
        /// <param name="copySubDir">���Ϊtrue,����Ŀ¼,���򲻰���</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            //���Ƶ�ǰĿ¼�ļ�
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));

                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(sourceFileName, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }
            //������Ŀ¼
            if (copySubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    CopyFiles(sourceSubDir, targetSubDir, overWrite, true);
                }
            }
        }

        /// <summary>
        /// ����ָ��Ŀ¼�������ļ�,��������Ŀ¼
        /// </summary>
        /// <param name="sourceDir">ԭʼĿ¼</param>
        /// <param name="targetDir">Ŀ��Ŀ¼</param>
        /// <param name="overWrite">���Ϊtrue,����ͬ���ļ�,���򲻸���</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite)
        {
            MoveFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// ����ָ��Ŀ¼�������ļ�
        /// </summary>
        /// <param name="sourceDir">ԭʼĿ¼</param>
        /// <param name="targetDir">Ŀ��Ŀ¼</param>
        /// <param name="overWrite">���Ϊtrue,����ͬ���ļ�,���򲻸���</param>
        /// <param name="moveSubDir">���Ϊtrue,����Ŀ¼,���򲻰���</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            //�ƶ���ǰĿ¼�ļ�
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                if (File.Exists(targetFileName))
                {
                    if (overWrite == true)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Delete(targetFileName);
                        File.Move(sourceFileName, targetFileName);
                    }
                }
                else
                {
                    File.Move(sourceFileName, targetFileName);
                }
            }
            if (moveSubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    MoveFiles(sourceSubDir, targetSubDir, overWrite, true);
                    Directory.Delete(sourceSubDir);
                }
            }
        }

        /// <summary>
        /// ɾ��ָ��Ŀ¼�������ļ�����������Ŀ¼
        /// </summary>
        /// <param name="targetDir">����Ŀ¼</param>
        public static void DeleteFiles(string targetDir)
        {
            DeleteFiles(targetDir, false);
        }

        /// <summary>
        /// ɾ��ָ��Ŀ¼�������ļ�����Ŀ¼
        /// </summary>
        /// <param name="targetDir">����Ŀ¼</param>
        /// <param name="delSubDir">���Ϊtrue,��������Ŀ¼�Ĳ���</param>
        public static void DeleteFiles(string targetDir, bool delSubDir)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                File.SetAttributes(fileName, FileAttributes.Normal);
                File.Delete(fileName);
            }
            if (delSubDir)
            {
                DirectoryInfo dir = new DirectoryInfo(targetDir);
                foreach (DirectoryInfo subDi in dir.GetDirectories())
                {
                    DeleteFiles(subDi.FullName, true);
                    subDi.Delete();
                }
            }
        }

        /// <summary>
        /// ����ָ��Ŀ¼
        /// </summary>
        /// <param name="targetDir"></param>
        public static void CreateDirectory(string targetDir)
        {
            DirectoryInfo dir = new DirectoryInfo(targetDir);
            if (!dir.Exists)
                dir.Create();
        }

        /// <summary>
        /// ������Ŀ¼
        /// </summary>
        /// <param name="targetDir">Ŀ¼·��</param>
        /// <param name="subDirName">��Ŀ¼����</param>
        public static void CreateDirectory(string parentDir, string subDirName)
        {
            CreateDirectory(parentDir + PATH_SPLIT_CHAR + subDirName);
        }

        /// <summary>
        /// ɾ��ָ��Ŀ¼
        /// </summary>
        /// <param name="targetDir">Ŀ¼·��</param>
        public static void DeleteDirectory(string targetDir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(targetDir);
            if (dirInfo.Exists)
            {
                DeleteFiles(targetDir, true);
                dirInfo.Delete(true);
            }
        }

        /// <summary>
        /// ɾ��ָ��Ŀ¼��������Ŀ¼,�������Ե�ǰĿ¼�ļ���ɾ��
        /// </summary>
        /// <param name="targetDir">Ŀ¼·��</param>
        public static void DeleteSubDirectory(string targetDir)
        {
            foreach (string subDir in Directory.GetDirectories(targetDir))
            {
                DeleteDirectory(subDir);
            }
        }

        /// <summary>
        /// ��ָ��Ŀ¼�µ���Ŀ¼���ļ�����xml�ĵ�
        /// </summary>
        /// <param name="targetDir">��Ŀ¼</param>
        /// <returns>����XmlDocument����</returns>
        public static XmlDocument CreateXml(string targetDir)
        {
            XmlDocument myDocument = new XmlDocument();
            XmlDeclaration declaration = myDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            myDocument.AppendChild(declaration);
            XmlElement rootElement = myDocument.CreateElement(targetDir.Substring(targetDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
            myDocument.AppendChild(rootElement);
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PATH_SPLIT_CHAR) + 1);
                rootElement.AppendChild(childElement);
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                rootElement.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
            return myDocument;
        }

        /// <summary>
        /// ����Xml��֧
        /// </summary>
        /// <param name="targetDir">��Ŀ¼</param>
        /// <param name="xmlNode">��Ŀ¼XmlDocument</param>
        /// <param name="myDocument">XmlDocument����</param>
        private static void CreateBranch(string targetDir, XmlElement xmlNode, XmlDocument myDocument)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PATH_SPLIT_CHAR) + 1);
                xmlNode.AppendChild(childElement);
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                xmlNode.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
        }
    }
}