using System;
using System.Collections.Generic;

namespace BarcodeMaster.Codes.QR.Writer
{
    /// <summary>
    /// Class Templater creates QR code template to draw QR code
    /// </summary>
    class QRTemplater
    {
        #region Alignment patterns
        static Dictionary<int, byte[]> alignmentPatterns = new Dictionary<int, byte[]>()
        {
            { 1, new byte[]{ 21 } },
            { 2, new byte[]{ 18 } },
            { 3, new byte[]{ 22 } },
            { 4, new byte[]{ 26 } },
            { 5, new byte[]{ 30 } },
            { 6, new byte[]{ 34 } },
            { 7, new byte[]{ 6, 22, 38 } },
            { 8, new byte[]{ 6, 24, 42 } },
            { 9, new byte[]{ 6, 26, 46 } },
            { 10, new byte[]{ 6, 28, 50 } },
            { 11, new byte[]{ 6, 30, 54 } },
            { 12, new byte[]{ 6, 32, 58 } },
            { 13, new byte[]{ 6, 34, 62 } },
            { 14, new byte[]{ 6, 26, 46, 66 } },
            { 15, new byte[]{ 6, 26, 48, 70 } },
            { 16, new byte[]{ 6, 26, 50, 74 } },
            { 17, new byte[]{ 6, 30, 54, 78 } },
            { 18, new byte[]{ 6, 30, 56, 82 } },
            { 19, new byte[]{ 6, 30, 58, 86 } },
            { 20, new byte[]{ 6, 34, 62, 90 } },
            { 21, new byte[]{ 6, 28, 50, 72, 94 } },
            { 22, new byte[]{ 6, 26, 50, 74, 98 } },
            { 23, new byte[]{ 6, 30, 54, 78, 102 } },
            { 24, new byte[]{ 6, 28, 54, 80, 106 } },
            { 25, new byte[]{ 6, 32, 58, 84, 110 } },
            { 26, new byte[]{ 6, 30, 58, 86, 114 } },
            { 27, new byte[]{ 6, 34, 62, 90, 118 } },
            { 28, new byte[]{ 6, 26, 50, 74, 98, 122 } },
            { 29, new byte[]{ 6, 30, 54, 78, 102, 126 } },
            { 30, new byte[]{ 6, 26, 52, 78, 104, 130 } },
            { 31, new byte[]{ 6, 30, 56, 82, 108, 134 } },
            { 32, new byte[]{ 6, 34, 60, 86, 112, 138 } },
            { 33, new byte[]{ 6, 30, 58, 86, 114, 142 } },
            { 34, new byte[]{ 6, 34, 62, 90, 118, 146 } },
            { 35, new byte[]{ 6, 30, 54, 78, 102, 126, 150 } },
            { 36, new byte[]{ 6, 24, 50, 76, 102, 128, 154 } },
            { 37, new byte[]{ 6, 28, 54, 80, 106, 132, 158 } },
            { 38, new byte[]{ 6, 32, 58, 84, 110, 136, 162 } },
            { 39, new byte[]{ 6, 26, 54, 82, 110, 138, 166 } },
            { 40, new byte[]{ 6, 30, 58, 86, 114, 142, 170 } },
        };
        #endregion

        #region Version codes
        static Dictionary<int, byte[]> versionCodes = new Dictionary<int, byte[]>()
        {
            { 7, new byte[]{ 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 0 } },
            { 8, new byte[]{ 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0 } },
            { 9, new byte[]{ 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 } },
            { 10, new byte[]{ 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 } },
            { 11, new byte[]{ 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0 } },
            { 12, new byte[]{ 0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0 } },
            { 13, new byte[]{ 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0 } },
            { 14, new byte[]{ 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0 } },
            { 15, new byte[]{ 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0 } },
            { 16, new byte[]{ 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0 } },
            { 17, new byte[]{ 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0 } },
            { 18, new byte[]{ 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0 } },
            { 19, new byte[]{ 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0 } },
            { 20, new byte[]{ 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0 } },
            { 21, new byte[]{ 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0 } },
            { 22, new byte[]{ 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 0 } },
            { 23, new byte[]{ 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 } },
            { 24, new byte[]{ 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0 } },
            { 25, new byte[]{ 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0 } },
            { 26, new byte[]{ 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0 } },
            { 27, new byte[]{ 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0 } },
            { 28, new byte[]{ 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0 } },
            { 29, new byte[]{ 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0 } },
            { 30, new byte[]{ 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 } },
            { 31, new byte[]{ 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0 } },
            { 32, new byte[]{ 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1 } },
            { 33, new byte[]{ 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1 } },
            { 34, new byte[]{ 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1 } },
            { 35, new byte[]{ 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1 } },
            { 36, new byte[]{ 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 } },
            { 37, new byte[]{ 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1 } },
            { 38, new byte[]{ 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1 } },
            { 39, new byte[]{ 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1 } },
            { 40, new byte[]{ 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 } }

        };
        #endregion

        #region Mask and correction level codes
        byte[][] maskCodesL =
        {
            new byte[]{ 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0 },
            new byte[]{ 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1 },
            new byte[]{ 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0 },
            new byte[]{ 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 1 },
            new byte[]{ 1, 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1 },
            new byte[]{ 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
            new byte[]{ 1, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
            new byte[]{ 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0 },
        };
        byte[][] maskCodesM =
        {
            new byte[]{ 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
            new byte[]{ 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 },
            new byte[]{ 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
            new byte[]{ 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1 },
            new byte[]{ 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1 },
            new byte[]{ 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1 ,1, 0 },
            new byte[]{ 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1 },
            new byte[]{ 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
        };
        byte[][] maskCodesQ =
        {
            new byte[]{ 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
            new byte[]{ 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0 },
            new byte[]{ 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1 },
            new byte[]{ 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0 },
            new byte[]{ 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0 },
            new byte[]{ 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1 },
            new byte[]{ 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0 },
            new byte[]{ 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
        };
        byte[][] maskCodesH =
        {
            new byte[]{ 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1 },
            new byte[]{ 0, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0 },
            new byte[]{ 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1 },
            new byte[]{ 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0 },
            new byte[]{ 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 0 },
            new byte[]{ 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
            new byte[]{ 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0 },
            new byte[]{ 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 },
        };
        #endregion

        public QRTemplater(CorrectionLevel correctionLevel, int version)
        {
            CorrectionLevel = correctionLevel;
            Version = version;
        }

        internal CorrectionLevel CorrectionLevel { get; set; }

        internal int Version { get; set; }

        /// <summary>
        /// Method CreateQRTemplate creates template to draw QRCode
        /// </summary>
        /// <param name="data">converted data</param>
        /// <returns>Returns template to draw QRCode</returns>
        internal byte[,] CreateQRTemplate(byte[] data)
        {
            byte[,] template;
            byte[,] QRtemplate;
            byte[][,] QRtemplates;

            template = CreateTemplate(Version);
            QRtemplates = CreateQRTemplates(data, template);
            QRtemplate = ChoosingBetterMask(QRtemplates);
            return AddStyleCutomization(QRtemplate);
        }

        /// <summary>
        /// Method CreateTemplate creates template for QR code template
        /// </summary>
        /// <param name="version">QR code version</param>
        private byte[,] CreateTemplate(int version)
        {
            //Initialize Template
            int size = GetSchemeSize(version);
            byte[,] template = new byte[size, size];

            //Search patterns
            CloseSearchPatterns(template);
            //Alignment patterns
            CloseAlignmentPatterns(template, version);
            //Sync strips
            CloseSyncStrips(template);
            //Mask
            CloseMaskCode(template);
            //VersionCode
            CloseVersionCode(template, version);

            return template;
        }

        /// <summary>
        /// Method CreateQRTemplates creates filled template to draw QRCode
        /// </summary>
        /// <param name="data">converted data</param>
        /// <param name="template">template to fill data</param>
        /// <returns>Returns filled templates</returns>
        private byte[][,] CreateQRTemplates(byte[] data, byte[,] template)
        {
            template = AddData(data, template);
            byte[][,] QRtemplates = ApplyMasks(template);
            for (int maskId = 0; maskId < QRtemplates.Length; maskId++)
            {
                AddBaseElements(QRtemplates[maskId], maskId);
            }
            return QRtemplates;
        }

        /// <summary>
        /// Method ChoosingBetterMask chose QR code with better mask
        /// </summary>
        /// <param name="QRTemplates">templates of QR code for each mask</param>
        /// <returns>Returns template with better mask</returns>
        private byte[,] ChoosingBetterMask(byte[][,] QRTemplates)
        {
            int totalPoints;
            int minPoints;
            int id;

            minPoints = int.MaxValue;
            id = 0;
            for (int maskId = 0; maskId < QRTemplates.Length; maskId++)
            {
                totalPoints = 0;
                totalPoints += FirstRule(QRTemplates[maskId]);
                totalPoints += SecondRule(QRTemplates[maskId]);
                totalPoints += ThirdRule(QRTemplates[maskId]);
                totalPoints += ForthRule(QRTemplates[maskId]);

                if(totalPoints < minPoints)
                {
                    id = maskId;
                    minPoints = totalPoints;
                } 
            }

            return QRTemplates[id];
        }

        /// <summary>
        /// Method CreateQRCode adds base elements on QR code template
        /// </summary>
        /// <param name="template">QR code template</param>
        /// <param name="maskId">mask id</param>
        private void AddBaseElements(byte[,] template, int maskId)
        {
            //Search patterns
            AddSearchPatterns(template, 1, 1);
            //Alignment patterns
            AddAlignmentPatterns(template, Version, 1, 1);
            //Sync strips
            AddSyncStrips(template);
            //Mask
            AddMaskCode(template, CorrectionLevel, maskId);
            //VersionCode
            AddVersionCode(template, Version);
        }

        /// <summary>
        /// Method AddStyleCutomization adds style customization for search and alignment patterns
        /// </summary>
        /// <param name="QRtemplate">QR code template</param>
        private byte[,] AddStyleCutomization(byte[,] QRtemplate)
        {
            #region Value of Patterns to custome style
            //SearchPattern
            byte searchOuter = 2;
            byte searchCenter = 3;
            //AlignmentPattern
            byte alignmentOuter = 4;
            byte alignmentCenter = 5;
            #endregion

            AddSearchPatterns(QRtemplate, searchOuter, searchCenter);
            AddAlignmentPatterns(QRtemplate, Version, alignmentOuter, alignmentCenter);

            return QRtemplate;
        }

        /// <summary>
        /// Method GetSchemeSize calculates size of QR code template
        /// </summary>
        /// <param name="version">QR code version</param>
        /// <returns>Returns size of QR code template</returns>
        private int GetSchemeSize(int version)
        {
            int size;
            byte[] alignmentPattern;

            alignmentPattern = alignmentPatterns[version];
            size = alignmentPattern[alignmentPattern.Length - 1];

            if (version > 1)
                size += 7;

            return size;
        }


        #region Search patterns
        /// <summary>
        /// Method AddSearchPatterns adds search patterns on the QR code template
        /// </summary>
        /// <param name="template">QR code template</param>
        /// <param name="outer">value to define outer contour of search pattern on template</param>
        /// <param name="center">value to define central block of search pattern on template</param>
        private void AddSearchPatterns(byte[,] template, byte outer, byte center)
        {
            int schemeSize = template.GetLength(0) - 1;

            //Outer contour
            for (int i = 0; i < 7; i++)
            {
                //UpLeft pattern
                template[0, i] = outer;
                template[i, 0] = outer;
                template[6, i] = outer;
                template[i, 6] = outer;

                //UpRight pattern
                template[0, schemeSize - i] = outer;
                template[i, schemeSize] = outer;
                template[6, schemeSize - i] = outer;
                template[i, schemeSize - 6] = outer;

                //DownLeft pattern
                template[schemeSize - i, 0] = outer;
                template[schemeSize, i] = outer;
                template[schemeSize - i, 6] = outer;
                template[schemeSize - 6, i] = outer;
            }

            //Inner contour
            for (int i = 1; i < 6; i++)
            {
                //UpLeft pattern
                template[1, i] = 0;
                template[i, 1] = 0;
                template[5, i] = 0;
                template[i, 5] = 0;

                ////UpRight pattern
                template[1, schemeSize - i] = 0;
                template[i, schemeSize - 1] = 0;
                template[5, schemeSize - i] = 0;
                template[i, schemeSize - 5] = 0;

                ////DownLeft pattern
                template[schemeSize - i, 1] = 0;
                template[schemeSize - 1, i] = 0;
                template[schemeSize - i, 5] = 0;
                template[schemeSize - 5, i] = 0;
            }

            //Central
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //UpLeft pattern
                    template[2 + i, 2 + j] = center;
                    //UpRight pattern
                    template[2 + i, schemeSize - 2 - j] = center;
                    //DownLeft pattern
                    template[schemeSize - 2 - j, 2 + i] = center;
                }
            }

            //Borders
            for (int i = 0; i < 8; i++)
            {
                //UpLeft pattern
                template[i, 7] = 0;
                template[7, i] = 0;
                //UpRight pattern
                template[i, schemeSize - 7] = 0;
                template[7, schemeSize - i] = 0;
                //DownLeft pattern
                template[schemeSize - 7, i] = 0;
                template[schemeSize - i, 7] = 0;
            }


        }

        /// <summary>
        /// Method CloseSearchPatterns closes area of search patterns
        /// </summary>
        /// <param name="template">Template template</param>
        private void CloseSearchPatterns(byte[,] template)
        {
            int schemeSize = template.GetLength(0) - 1;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //UpLeft pattern
                    template[i, j] = 1;
                    //UpRight pattern
                    template[i, schemeSize - j] = 1;
                    //DownLeft pattern
                    template[schemeSize - i, j] = 1;
                }
            }
        }
        #endregion

        #region Alignment patterns
        /// <summary>
        /// Method AddAlignmentPatterns adds alignment patterns on the QR code template
        /// </summary>
        /// <param name="template">QR code template</param>
        /// <param name="version">QR code version</param>
        /// <param name="outer">value to define outer contour of alignment pattern on template</param>
        /// <param name="center">value to define central block of alignment pattern on template</param>
        private void AddAlignmentPatterns(byte[,] template, int version, byte outer, byte center)
        {
            if (version == 1)
                return;

            int[,] points;
            int countPoints;
            int x, y;

            points = GetPatternPoints(version);
            countPoints = points.GetLength(0);

            for (int pointId = 0; pointId < countPoints; pointId++)
            {
                y = points[pointId, 0];
                x = points[pointId, 1];
                for (int i = y - 2; i <= y + 2; i++)
                {
                    for (int j = x - 2; j <= x + 2; j++)
                    {
                        template[i, j] = 1;
                    }
                }
            }
            //Inner contour
            for (int pointId = 0; pointId < countPoints; pointId++)
            {
                y = points[pointId, 0];
                x = points[pointId, 1];
                for (int i = y - 1; i <= y + 1; i++)
                {
                    for (int j = x - 1; j <= x + 1; j++)
                    {
                        template[i, j] = 0;
                    }
                }
            }
            //Central
            for (int i = 0; i < countPoints; i++)
            {
                template[points[i, 0], points[i, 1]] = 1;
            }
        }

        /// <summary>
        /// Method CloseAlignmentPatterns closes area of alignment patterns
        /// </summary>
        /// <param name="template">Template template</param>
        /// <param name="version">QR code version</param>
        private void CloseAlignmentPatterns(byte[,] template, int version)
        {
            if (version == 1)
                return;

            int[,] points;
            int countPoints;
            int x, y;

            points = GetPatternPoints(version);
            countPoints = points.GetLength(0);

            for (int pointId = 0; pointId < countPoints; pointId++)
            {
                y = points[pointId, 0];
                x = points[pointId, 1];
                for (int i = y - 2; i <= y + 2; i++)
                {
                    for (int j = x - 2; j <= x + 2; j++)
                    {
                        template[i, j] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Method GetPatternPoints calculates positions of alignment patterns
        /// </summary>
        /// <param name="version">QR code version</param>
        /// <returns>Returns calculated positions of alignment patterns</returns>
        private int[,] GetPatternPoints(int version)
        {
            int[,] points;
            byte[] patternPoints;
            int countPoints;
            int index;


            patternPoints = alignmentPatterns[version];
            countPoints = (int)Math.Pow(patternPoints.Length, 2);
            if (version >= 7)
                countPoints -= 3;
            points = new int[countPoints, 2];
            index = 0;
            for (int i = 0; i < patternPoints.Length; i++)
            {
                for (int j = 0; j < patternPoints.Length; j++)
                {
                    if (!IsValidPoint(i, j, version, patternPoints.Length))
                        continue;
                    points[index, 0] = patternPoints[i];
                    points[index, 1] = patternPoints[j];
                    index++;
                }
            }

            return points;
        }

        /// <summary>
        /// Method IsValidPoint checks points on valid
        /// </summary>
        /// <param name="x">column</param>
        /// <param name="y">row</param>
        /// <param name="version">QR code version</param>
        /// <param name="lastValue">last value from array of alignment patterns</param>
        /// <returns>Returns true if position is valid and false if not</returns>
        private bool IsValidPoint(int x, int y, int version, int lastValue)
        {
            if (version < 7)
                return true;
            if (x == 0 && y == 0)
                return false;
            lastValue--;
            if (x == 0 && y == lastValue)
                return false;
            if (x == lastValue && y == 0)
                return false;
            return true;
        }
        #endregion

        #region Sync strips
        /// <summary>
        /// Method AddSyncStrips adds sync strips on the QR code template
        /// </summary>
        /// <param name="template">QR code template</param>
        private void AddSyncStrips(byte[,] template)
        {
            int schemeSize = template.GetLength(0);

            for (int i = 8; i < schemeSize - 8; i++)
            {
                template[6, i] = (byte)((i + 1) % 2);
                template[i, 6] = (byte)((i + 1) % 2);
            }
        }

        /// <summary>
        /// Method CloseSyncStrips closes area of sync strips
        /// </summary>
        /// <param name="template">Template template</param>
        private void CloseSyncStrips(byte[,] template)
        {
            int schemeSize = template.GetLength(0);

            for (int i = 8; i < schemeSize - 8; i++)
            {
                template[6, i] = 1;
                template[i, 6] = 1;
            }
        }
        #endregion

        #region Mask code
        /// <summary>
        /// Method AddMaskCode adds mask code on the QRCode template
        /// </summary>
        /// <param name="template">QR code template</param>
        /// <param name="correctionLevel">QR code correction level</param>
        /// <param name="maskId">QR code mask id</param>
        private void AddMaskCode(byte[,] template, CorrectionLevel correctionLevel, int maskId)
        {
            int schemeSize = template.GetLength(0) - 1;
            int point = 0;
            byte[] mask;

            mask = GetMaskCode(correctionLevel, maskId);
            for (int i = 0; i < 8; i++)
            {
                if (i == 6)
                    point++;
                template[8, point] = mask[i];
                template[point, 8] = mask[mask.Length - 1 - i];
                point++;

                template[schemeSize - i, 8] = mask[i];
                template[8, schemeSize - i] = mask[mask.Length - 1 - i];
            }

            template[schemeSize - 7, 8] = 1;
        }

        /// <summary>
        /// Method CloseMaskCode closes area of mask code
        /// </summary>
        /// <param name="template">Template template</param>
        private void CloseMaskCode(byte[,] template)
        {
            int schemeSize = template.GetLength(0) - 1;
            int point = 0;

            for (int i = 0; i < 8; i++)
            {
                if (i == 6)
                    point++;
                template[8, point] = 1;
                template[point, 8] = 1;
                point++;

                template[schemeSize - i, 8] = 1;
                template[8, schemeSize - i] = 1;
            }
        }

        /// <summary>
        /// Method GetMaskCode gets QR code mask as bit sequence
        /// </summary>
        /// <param name="correctionLevel">QR code correction level</param>
        /// <param name="maskId">QR code mask id</param>
        /// <returns>Returns mask as bit sequence</returns>
        private byte[] GetMaskCode(CorrectionLevel correctionLevel, int maskId)
        {
            if (maskId > 7)
                maskId = 0;
            switch (correctionLevel)
            {
                case CorrectionLevel.L:
                    return maskCodesL[maskId];
                case CorrectionLevel.M:
                    return maskCodesM[maskId];
                case CorrectionLevel.Q:
                    return maskCodesQ[maskId];
                case CorrectionLevel.H:
                    return maskCodesH[maskId];
                default:
                    return default;
            }
        }
        #endregion

        #region Version Code
        /// <summary>
        /// Method AddVersionCode adds version code on the Qr code
        /// </summary>
        /// <param name="template">QR code template</param>
        /// <param name="version">QR code version</param>
        private void AddVersionCode(byte[,] template, int version)
        {
            if (version < 7)
                return;

            byte[] versionCode = versionCodes[version];
            int schemeSize = template.GetLength(0) - 1;

            for (int i = 0; i < 6; i++)
            {
                template[schemeSize - 10, i] = versionCode[i];
                template[schemeSize - 9, i] = versionCode[6 + i];
                template[schemeSize - 8, i] = versionCode[12 + i];

                template[i, schemeSize - 10] = versionCode[i];
                template[i, schemeSize - 9] = versionCode[6 + i]; ;
                template[i, schemeSize - 8] = versionCode[12 + i];
            }
        }

        /// <summary>
        /// Method CloseVersionCode closes area of version code
        /// </summary>
        /// <param name="template">Template template</param>
        /// <param name="version">QR code version</param>
        private void CloseVersionCode(byte[,] template, int version)
        {
            if (version < 7)
                return;

            int schemeSize = template.GetLength(0) - 1;

            for (int i = 0; i < 6; i++)
            {
                template[schemeSize - 10, i] = 1;
                template[schemeSize - 9, i] = 1;
                template[schemeSize - 8, i] = 1;

                template[i, schemeSize - 10] = 1;
                template[i, schemeSize - 9] = 1;
                template[i, schemeSize - 8] = 1;
            }
        }
        #endregion

        #region Data
        /// <summary>
        /// Method AddData apply data on template 
        /// </summary>
        /// <param name="data">converted data</param>
        /// <param name="template">QR code template</param>
        /// <returns>Returns a template with apply converted data</returns>
        private byte[,] AddData(byte[] data, byte[,] template)
        {
            int border;
            int row, column;
            int delRow, delColumn;
            int dataId;

            border = template.GetLength(0) - 1;
            row = template.GetLength(0) - 1;
            column = template.GetLength(1) - 1;
            delRow = -1;
            delColumn = -1;
            dataId = 0;

            while (column >= 0)
            {
                if (template[row, column] == 0)
                {
                    if (dataId < data.Length)
                    {
                        template[row, column] = (byte)(data[dataId]);
                        dataId++;
                    }
                    else
                        template[row, column] = 0;
                }

                if (delColumn == 1)
                    row += delRow;
                column += delColumn;
                delColumn *= -1;

                if (row < 0 || row > border)
                {
                    delRow *= -1;
                    row += delRow;
                    column -= 2;
                    if (column == 6)
                        column--;
                }
            }

            return template;
        }

        /// <summary>
        /// Method ApplyMasks apply masks to QR code template
        /// </summary>
        /// <param name="template">Qr code template</param>
        /// <returns>Returns QR templates with apply all masks</returns>
        private byte[][,] ApplyMasks(byte[,] template)
        {
            byte[][,] QRtemplates = new byte[8][,];

            for (int maskId = 0; maskId < 8; maskId++)
            {
                QRtemplates[maskId] = new byte[template.GetLength(0), template.GetLength(1)];

                for (int y = 0; y < template.GetLength(0); y++)
                {
                    for (int x = 0; x < template.GetLength(1); x++)
                    {
                        QRtemplates[maskId][y, x] = ApplyMask(template[y, x], maskId, x, y);
                    }
                }
            }
            return QRtemplates;
        }

        /// <summary>
        /// Method ApplyMask apply mask to specific module
        /// </summary>
        /// <param name="bit">value of modul</param>
        /// <param name="maskId">id applied mask</param>
        /// <param name="x">modul column position</param>
        /// <param name="y">modul row position</param>
        /// <returns>Returns value of modul</returns>
        private byte ApplyMask(byte bit, int maskId, int x, int y)
        {
            byte value = bit;
            switch (maskId)
            {
                case 0:
                    if ((x + y) % 2 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 1:
                    if (y % 2 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 2:
                    if (x % 3 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 3:
                    if ((x + y) % 3 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 4:
                    if ((x / 3 + y / 2) % 2 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 5:
                    if ((x * y) % 2 + (x * y) % 3 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 6:
                    if (((x * y) % 2 + (x * y) % 3) % 2 == 0)
                        value = (byte)(~value & 1);
                    break;
                case 7:
                    if (((x * y) % 3 + (x + y) % 2) % 2 == 0)
                        value = (byte)(~value & 1);
                    break;
            }
            return value;
        }
        #endregion


        #region Rules
        /// <summary>
        /// Method FirstRule calculates the number of points according to the specified rule
        /// </summary>
        /// <param name="QRtemplate">template QR code</param>
        /// <returns>Returns calculated the number of points</returns>
        private int FirstRule(byte[,] QRtemplate)
        {
            int totalPoints;
            int points;
            int current;

            totalPoints = 0;
            //Horizontal
            for (int row = 0; row < QRtemplate.GetLength(0); row++)
            {
                current = QRtemplate[row, 0];
                points = 1;

                for (int column = 1; column < QRtemplate.GetLength(1); column++)
                {

                    if (QRtemplate[row, column] != current)
                    {
                        if (points >= 5)
                        {
                            totalPoints += points - 2;
                        }
                        current = QRtemplate[row, column];
                        points = 1;
                        continue;
                    }
                    points++;
                    if (column == QRtemplate.GetLength(1) - 1)
                        if (points >= 5)
                        {
                            totalPoints += points - 2;
                        }
                }
            }
            //Vetical
            for (int column = 0; column < QRtemplate.GetLength(1); column++)
            {
                current = QRtemplate[0, column];
                points = 1;
                for (int row = 1; row < QRtemplate.GetLength(0); row++)
                {
                    if (QRtemplate[row, column] != current)
                    {
                        if (points >= 5)
                        {
                            totalPoints += points - 2;
                        }
                        current = QRtemplate[row, column];
                        points = 1;
                        continue;
                    }
                    points++;
                    if (row == QRtemplate.GetLength(0) - 1)
                        if (points >= 5)
                        {
                            totalPoints += points - 2;
                        }
                }
            }

            return totalPoints;
        }
        /// <summary>
        /// Method FirstRule calculates the number of points according to the specified rule
        /// </summary>
        /// <param name="QRtemplate">template QR code</param>
        /// <returns>Returns calculated the number of points</returns>
        private int SecondRule(byte[,] QRtemplate)
        {
            int totalPoints;
            int current;

            totalPoints = 0;
            for (int row = 0; row < QRtemplate.GetLength(0); row++)
            {
                if (row == QRtemplate.GetLength(0) - 1)
                    break;
                for (int column = 0; column < QRtemplate.GetLength(1); column++)
                {
                    if (column == QRtemplate.GetLength(1) - 1)
                        continue;
                    current = QRtemplate[row, column] + QRtemplate[row, column + 1] + QRtemplate[row + 1, column] + QRtemplate[row + 1, column + 1];
                    if (current == 0 || current == 4)
                        totalPoints += 3;
                }
            }

            return totalPoints;
        }
        /// <summary>
        /// Method FirstRule calculates the number of points according to the specified rule
        /// </summary>
        /// <param name="QRtemplate">template QR code</param>
        /// <returns>Returns calculated the number of points</returns>
        private int ThirdRule(byte[,] QRtemplate)
        {
            int totalPoints;
            byte pattern;
            byte value;
            byte borderUL, borderRD;

            totalPoints = 0;
            pattern = 93;

            //Horizontal
            borderUL = 0;
            borderRD = 0;
            for (int row = 0; row < QRtemplate.GetLength(0); row++)
            {
                value = 0;
                for (int column = 0; column < QRtemplate.GetLength(1); column++)
                {
                    value = (byte)((value << 1) | QRtemplate[row, column] & 127);
                    if (value == pattern)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (column - 10 > 0)
                                if (QRtemplate[row, column - 7 - i] == 0)
                                    borderUL++;
                            if (column + 4 < QRtemplate.GetLength(1))
                                if (QRtemplate[row, column + 1 + i] == 0)
                                    borderRD++;
                        }
                        if (borderUL == 4 || borderRD == 4)
                            totalPoints += 40;
                        borderUL = 0;
                        borderRD = 0;
                    }
                }
            }

            //Vertical
            borderUL = 0;
            borderRD = 0;
            for (int column = 0; column < QRtemplate.GetLength(1); column++)
            {
                value = 0;
                for (int row = 0; row < QRtemplate.GetLength(0); row++)
                {
                    value = (byte)((value << 1) | QRtemplate[row, column] & 127);
                    if (value == pattern)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (row - 10 >= 0)
                                if (QRtemplate[row - 7 - i, column] == 0)
                                    borderUL++;
                            if (row + 4 < QRtemplate.GetLength(0))
                                if (QRtemplate[row + 1 + i, column] == 0)
                                    borderRD++;
                        }
                        if (borderUL == 4 || borderRD == 4)
                            totalPoints += 40;
                        borderUL = 0;
                        borderRD = 0;
                    }
                }
            }

            return totalPoints;
        }
        /// <summary>
        /// Method FirstRule calculates the number of points according to the specified rule
        /// </summary>
        /// <param name="QRtemplate">template QR code</param>
        /// <returns>Returns calculated the number of points</returns>
        private int ForthRule(byte[,] QRtemplate)
        {
            int totalPoints;
            int counter;
            decimal size;

            counter = 0;
            for (int row = 0; row < QRtemplate.GetLength(0); row++)
            {
                for (int column = 0; column < QRtemplate.GetLength(1); column++)
                {
                    if (QRtemplate[row, column] == 1)
                        counter++;
                }
            }

            size = QRtemplate.GetLength(0) * QRtemplate.GetLength(1);
            decimal temp = counter / size * 100 - 50;
            totalPoints = (int)Math.Abs(Math.Truncate(temp) * 2);

            return totalPoints;
        }
        #endregion

    }
}
