namespace Tasahil_.net_core_6_.Helper
{
    public static class UploadFile
    {
        public static string addFile(IFormFile file)
        {
            try
            {

                // 1 ) Get Directory

                 string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/Img" ;


                //2) Get File Name

                  string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);


                // 3) Merge Path with File Name

                 string FinalPath = Path.Combine(FolderPath, FileName);


                //4) Save File As Streams "Data Overtime"

                  using (var Stream = new FileStream(FinalPath, FileMode.Create))

                {

                 file.CopyTo(Stream);

                }
                return FileName;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
