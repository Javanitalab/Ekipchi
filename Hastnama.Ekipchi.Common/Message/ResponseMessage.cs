namespace Hastnama.Ekipchi.Common.Message
{
    public static class ResponseMessage
    {
        #region Common

        public const string BadRequestQuery = "ورودی های داده شده معتبر نیست";
        public const string InvalidPagingOption = "پیجینگ وارد شده معتبر نیست";
        public const string InvalidLocalId = "ایدی وارد شده معتبر نیست";
        public const string TokenNotFound = "توکن پیدا نشد";
        public const string InvalidActiveCode = "کد فعالسازی معتبر نمیباشد";
        public const string SendMessageFailed = "ارسال پیامک با مشکل برخورد کرد";


        #endregion

        #region File

        public const string FileNotFound = "فایل مورد نظر پیدا نشد";

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
        public const string WrongPassword = "گذرواژه صحیح نمیباشد";

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

        #region Message

        public const string MessageNotFound = " پیامی پیدا نشد";
        public const string SenderAndReceiverAreTheSame = " فرستنده و گیرنده نمیتوانند یکسان باشند";
        public const string ParentMessageNotFound = " پیام اولیه پیدا نشد";
        public const string ReceiverNotSet = " فرستنده مشخص نشده است";

        #endregion

        #region Group

        public const string DuplicateGroupName = " نام گروه تکراری است";
        public const string GroupNotFound = "گروه مورد نظر پیدا نشد";
        public const string InvalidGroupId = "ایدی گروه معتبر نمیباشد";
        public const string GroupNameIsInvalid = " نام گروه معتبر نمیباشد";
        public const string GroupOwnerIsInvalid = "مشخص کردن مدیر گروه الزامی است";

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

        #region Role

        public const string RoleNotFound = "نقش مورد نظر پیدا نشد";
        public const string RoleIsVitual = "نقش مورد ویتیال است";

        #endregion

        #region Host

        public const string HostNotFound = "میزبان مورد نظر پیدا نشد";
        public const string InvalidHostId = "آیدی میزبان معتبر نیست";
        public const string InvalidHostName = "نام میزبان معتبر نیست";
        public const string InvalidHostAvailableDates = "روزهای دردسترس میزبان معتبر نیست";

        #endregion

        #region Event

        public const string EventNotFound = "رویداد پیدا نشد";
        public const string InvalidEventName = "نام رویداد معتبر نیست";
        public const string InvalidEventSchedule = " زمانبندیه رویداد معتبر نیست";

        #endregion

        #region Password

        public const string ForgotPasswordNotAccepted = " درخواست تقییر رمز عبور مورد قبول نیست";
        public const string ForgotPasswordAccepted = "درخواست تقییر رمز عبور مورد قبول است";
        public const string PasswordSuccessfullyChanged = "پسورد با موفقیت تقییر کرد";
        
        #endregion

    }
}