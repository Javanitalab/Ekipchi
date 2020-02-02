namespace Hastnama.Ekipchi.Common.Message
{
    public static class PersianErrorMessage
    {
        #region Common

        public const string BadRequestQuery = "ورودی های داده شده معتبر نیست";
        public const string InvalidPagingOption = "پیجینگ وارد شده معتبر نیست";

        #endregion

        #region User

        public const string InvalidUserCredential = "گذرواؤه یا واژه کاربری معتبر نمیباشد";
        public const string UserIsDeActive = "حساب کاربری مورد نظر غیر فعال میباشد";
        public const string UserNotFound = "کاربر مورد نظر پیدا نشد";
        public const string UserAlreadyExist = "کاربری با واژه کاربری داده شده در سیستم موجئد است";
        public const string UnAuthorized = "شما دسترسی استفاده از این سرویس را ندارید";
        public const string InvalidEmailAddress = "ایمیل نامعتبر است";
        public const string InvalidMobile = "شماره موبایل نامعتبر است";
        public const string InvalidNameOrFamily = "نام یا نام خانوادگی معتبر نیست";
        public const string InvalidUserRole = "نقش کاربر نامعتبر است";
        public const string InvalidUserId = "ایدی کاربر معتبر نمیباشد";
        public const string EmailAddressAlreadyExist = "آدرس ایمیل در سیستم موجود میباشد";
        public const string MobileAlreadyExist = "شماره موبایل در سیستم موجود میباشد";
        public const string UsernameAlreadyExist = "نام کاربری در سیستم موجود میباشد";

        #endregion

        #region City

        public const string CityNotFound = "شهر مورد نظر پیدا نشد";
        public const string DuplicateCityName = "نام شهر تکراری است";
        public const string CityNameIsInvalid = "نام شهر نامعتبر است";
        public const string InvalidCityId = "ایدی شهر معتبر نمیباشد";

        #endregion

        #region County

        public const string InvalidCountyId = "ایدی شهرستان معتبر نمیباشد";
        public const string DuplicateCountyName = "نام شهرستان تکراری است";
        public const string CountyNotFound = "شهرستان مورد نظر پیدا نشد";


        #endregion

        #region Province

        public const string InvalidProvinceId = "ایدی استان معتبر نمیباشد";
        public const string DuplicateProvinceName = "نام استان تکراری است";
        public const string ProvinceNotFound = "استان مورد نظر پیدا نشد";
        public const string ProvinceNameIsInvalid = "نام استان نامعتبر است";


        #endregion


        #region Region

        
        public const string DuplicateRegionName = "نام منطقه تکراری است";
        public const string RegionNotFound = "منطقه مورد نظر پیدا نشد";
        public const string InvalidRegionId = "ایدی منطقه معتبر نمیباشد";
        public const string RegionNameIsInvalid = "نام منطقه نامعتبر است";

        #endregion

        #region Blog

        public const string DuplicateBlogTitle = "موضوع بلاگ تکراری است";
        public const string BlogNotFound = "بلاگ مورد نظر پیدا نشد";
        public const string InvalidBlogId = "ایدی بلاگ معتبر نمیباشد";
        public const string BlogTitleIsInvalid = "موضوع بلاگ نامعتبر است";

        #endregion

        #region BlogCategory

        
        public const string DuplicateBlogCategoryName = " نام دسته بندی بلاگ تکراری است";
        public const string BlogCategoryNotFound = "دسته بندی بلاگ مورد نظر پیدا نشد";
        public const string InvalidBlogCategoryId = "ایدی دسته بندی معتبر نمیباشد";
        public const string InvalidBlogCategorySlug = " شعار دسته بندی نامعتبر است";

        #endregion
        
        #region Category

        
        public const string DuplicateCategoryName = " نام دسته بندی تکراری است";
        public const string InvalidCategoryName = " نام دسته بندی معتبر نمیباشد";
        public const string CategoryNotFound = "دسته بندی مورد نظر پیدا نشد";
        public const string InvalidCategoryId = "ایدی دسته بندی معتبر نمیباشد";

        #endregion
        #region Comment

        
        public const string CommentNotFound = "کامنت مورد نظر پیدا نشد";
        public const string InvalidCommentId = "ایدی کامنت معتبر نمیباشد";
        public const string InvalidCommentContent = "محتوای کامنت معتبر نمیباشد";

        #endregion
        #region Faq

        
        public const string FaqNotFound = "کامنت مورد نظر پیدا نشد";
        public const string InvalidFaqId = "ایدی کامنت معتبر نمیباشد";
        public const string InvalidFaqQuestion = "محتوای کامنت معتبر نمیباشد";

        #endregion
        #region Coupan

        
        public const string CouponNotFound = "کامنت مورد نظر پیدا نشد";
        public const string InvalidCouponId = "ایدی کامنت معتبر نمیباشد";
        public const string DuplicateCouponCode = "کد تخفیف تکراری است";
        public const string InvalidCouponCode = "کد تخفیف معتبر نمیباشد";

        #endregion
    }
}