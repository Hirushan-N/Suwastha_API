using Suwastha_API.Models;
using Suwastha_API.Services;

namespace Suwastha_API.Configs
{
    public static class IDManager
    {
        #region Create New IDs 
        public static string? GenarateNewIDFor(string tableName, String Prefix)
        {
            string newCode = "";
            string numaricPart = "";
            try
            {
                //get last code from db and split in into 2 parts
                IDMasterService iDMasterService = new IDMasterService();
                IDMaster iDMaster = iDMasterService.Read(tableName);
                string lastID = iDMaster.LastID;
                if (String.IsNullOrEmpty(lastID))
                {
                    newCode = Prefix + "-00000001";
                }
                else
                {
                    //split lastCode and get numaric part
                    string[] codeArray = lastID.Split("-");
                    for (int i = 0; i < codeArray.Length; i++)
                    {
                        numaricPart = codeArray[i];
                    }
                    //increment second part (numarics)
                    numaricPart = Convert.ToString(Convert.ToInt32(numaricPart) + 1);

                    int l = numaricPart.Length;
                    //create numaric part with correct format
                    for (int i = 0; i < (8 - l); i++)
                    {
                        numaricPart = "0" + numaricPart;
                    }
                    //concat
                    newCode = Prefix + "-" + numaricPart;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newCode;
        }
        #endregion
    }
}
