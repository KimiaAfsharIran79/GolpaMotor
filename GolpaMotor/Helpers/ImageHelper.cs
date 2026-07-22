namespace GolpaMotor.Helpers
{
    public static class ImageHelper
    {
        public static string FixProduct(string? fileName)
        {
            // اگر خالی بود
            if (string.IsNullOrWhiteSpace(fileName))
                return "/images/pics/noimage.jpg";

            // فقط اسم فایل (امنیت + جلوگیری از path injection)
            var safeFileName = Path.GetFileName(fileName);

            // مسیر نهایی ثابت
            return "/images/imageProducts/" + safeFileName;
        }

        public static string FixUser(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "/images/pics/nofile.jpg";

            return "/images/imageUsers/" + Path.GetFileName(fileName);
        }
    }
}
