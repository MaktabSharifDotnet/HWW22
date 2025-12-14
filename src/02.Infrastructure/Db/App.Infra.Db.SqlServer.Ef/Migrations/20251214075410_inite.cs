using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infra.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class inite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoleEnum = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inventory = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => new { x.CartId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartProducts_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "شامل انواع موبایل، لپ‌تاپ، تبلت و لوازم جانبی هوشمند", false, "کالای دیجیتال" },
                    { 2, "انواع لباس‌های مردانه، زنانه، بچگانه و اکسسوری‌های روز", false, "پوشاک و مد" },
                    { 3, "تجهیزات برقی آشپزخانه، دکوراسیون و وسایل کاربردی منزل", false, "لوازم خانگی" },
                    { 4, "کتاب‌های آموزشی، رمان، مجلات و انواع نوشت‌افزار", false, "کتاب و لوازم تحریر" },
                    { 5, "تجهیزات تخصصی ورزشی، لباس ورزشی و لوازم کمپینگ و سفر", false, "ورزش و سفر" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Image", "Inventory", "IsDeleted", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "گوشی موبایل اپل مدل iPhone 13 پرچمداری است که با تغییرات ظریف اما کلیدی نسبت به نسل قبل، تجربه‌ای بی‌نظیر را برای کاربران رقم می‌زند. این گوشی با ظرفیت 128 گیگابایت، نه تنها فضای کافی برای ذخیره هزاران عکس و ویدیو را فراهم می‌کند، بلکه با بهره‌گیری از تراشه قدرتمند A15 Bionic، سرعتی باورنکردنی در اجرای سنگین‌ترین بازی‌ها و اپلیکیشن‌های ویرایش ویدیو دارد. طراحی بدنه همچنان از لبه‌های تخت و شیشه‌ی مقاوم Ceramic Shield در جلو بهره می‌برد که مقاومت آن در برابر ضربه را چندین برابر کرده است.\r\n\r\nیکی از مهم‌ترین نقاط قوت آیفون 13، ارتقای چشمگیر دوربین‌های آن است. سنسورهای بزرگتر در دوربین عریض و فوق عریض باعث شده‌اند که جذب نور تا 47 درصد افزایش یابد؛ این یعنی عکس‌برداری در شب و محیط‌های کم‌نور با جزئیاتی خیره‌کننده همراه خواهد بود. قابلیت جدید و انقلابی Cinematic Mode نیز به شما اجازه می‌دهد ویدیوهایی با فوکوس هوشمند و عمق میدان سینمایی ضبط کنید، گویی که یک کارگردان حرفه‌ای پشت دوربین ایستاده است.\r\n\r\nصفحه‌نمایش Super Retina XDR این مدل اکنون 28 درصد روشنایی بیشتری دارد که کار با گوشی در زیر نور مستقیم خورشید را بسیار راحت‌تر کرده است. همچنین، اپل در این نسل توانسته است با بهینه‌سازی مصرف انرژی و افزایش ظرفیت فیزیکی باتری، عمر باتری را نسبت به آیفون 12 تا 2.5 ساعت افزایش دهد که برای کاربران پرمصرف خبری بسیار خوشحال‌کننده است. پشتیبانی از شبکه 5G، استاندارد ضدآب IP68 و تنوع رنگی جذاب، آیفون 13 را به یکی از کامل‌ترین گوشی‌های هوشمند بازار تبدیل کرده است که ترکیبی از زیبایی، قدرت و کارایی را ارائه می‌دهد.", "iphone13.jpg", 5, false, 45000000m, "گوشی موبایل آیفون 13" },
                    { 2, 1, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "لپ‌تاپ‌های سری TUF شرکت ایسوس همیشه نمادی از ترکیب قدرت سخت‌افزاری بالا و مقاومت فیزیکی بی‌نظیر بوده‌اند و مدل TUF Gaming F15 نیز از این قاعده مستثنی نیست. این دستگاه که برای گیمرها و طراحان گرافیک طراحی شده، بدنه‌ای با استاندارد نظامی دارد که در برابر ضربه، لرزش و تغییرات دما مقاوم است. ظاهر دستگاه با خطوط شکسته‌ و نورپردازی کیبورد RGB، حس یک ابزار جنگی مدرن را به کاربر القا می‌کند.\r\n\r\nدر دل این لپ‌تاپ، پردازنده قدرتمند نسل جدید اینتل به همراه کارت گرافیک سری NVIDIA GeForce RTX قرار گرفته است که اجرای روان جدیدترین بازی‌های روز دنیا با تنظیمات گرافیکی بالا را تضمین می‌کند. سیستم خنک‌کننده پیشرفته با لوله‌های حرارتی مسی و فن‌های چندگانه تضمین می‌کند که حتی در طولانی‌ترین جلسات بازی یا رندرهای سنگین، دمای قطعات در حد ایده‎‌آل باقی بماند و خبری از افت فریم نباشد.\r\n\r\nصفحه‌نمایش 15.6 اینچی این محصول با نرخ بروزرسانی بالا (Refresh Rate 144Hz)، تصاویری نرم و بدون پرش را نمایش می‌دهد که در بازی‌های شوتر و سرعتی، برتری قابل توجهی به گیمر می‌دهد. علاوه بر گیمینگ، این لپ‌تاپ به لطف رم بالا و حافظه SSD پرسرعت، گزینه‌ای عالی برای برنامه‌نویسان و تدوینگران ویدیو محسوب می‌شود. پورت‌های کامل شامل USB Type-C، HDMI و پورت شبکه، نیاز شما به دانگل‌های اضافی را از بین می‌برد و باتری قدرتمند آن نیز برای کارهای روزمره شارژدهی قابل قبولی دارد.", "asus_laptop.jpg", 0, false, 38000000m, "لپ تاپ ایسوس TUF Gaming" },
                    { 3, 2, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "کفش مخصوص دویدن نایکی مدل Revolution 6، تعریف جدیدی از راحتی و انعطاف‌پذیری را برای دوندگان و ورزشکاران ارائه می‌دهد. این مدل که ادامه‌دهنده راه موفق نسل‌های قبلی سری رولوشن است، با رویکردی مینیمالیستی اما کاربردی طراحی شده است. رویه‌ی کفش از پارچه‌ی مش (Mesh) تنفس‌پذیر ساخته شده که گردش هوای عالی را درون کفش ایجاد می‌کند؛ این ویژگی باعث می‌شود حتی پس از ساعت‌ها دویدن یا پیاده‌روی، پای شما خشک و خنک باقی بماند و از بوی بد جلوگیری شود.\r\n\r\nمهم‌ترین ویژگی Revolution 6، استفاده از فوم بسیار نرم در لایه‌ی میانی است که ضربات وارده از سطح زمین را به خوبی جذب می‌کند. طراحی کامپیوتری زیره بیرونی باعث شده تا هنگام برخورد پا با زمین، حالتی فنری ایجاد شود و فشار کمتری به زانوها و کمر وارد گردد. این زیره همچنین دارای الگوی خاصی است که چسبندگی بسیار بالایی روی سطوح مختلف (آسفالت، تردمیل و زمین خاکی) ایجاد می‌کند و خطر لیز خوردن را به حداقل می‌رساند.\r\n\r\nعلاوه بر کارایی فنی، نایکی در تولید این مدل به محیط زیست نیز توجه داشته و بخشی از مواد اولیه آن از پلاستیک‌های بازیافتی تهیه شده است. طراحی پاشنه تقویت‌شده باعث می‌شود پا در هنگام حرکت ثابت بماند و از پیچ‌خوردگی جلوگیری شود. این کفش با ظاهر اسپرت و مدرن خود، نه تنها برای ورزش، بلکه برای استفاده روزمره و ست کردن با لباس‌های کژوال (مانند شلوار جین یا اسلش) نیز گزینه‌ای بسیار شیک و جذاب است.", "nike_shoes.jpg", 20, false, 2500000m, "کفش ورزشی نایکی Revolution 6" },
                    { 4, 3, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "یخچال و فریزر دوقلو 40 فوت پارس، یکی از محبوب‌ترین و باکیفیت‌ترین محصولات برودتی ساخت ایران است که با در نظر گرفتن نیازهای خانواده‌های پرجمعیت ایرانی طراحی شده است. این محصول شامل دو دستگاه جداگانه (یک یخچال کامل و یک فریزر کامل) است که می‌توانند به هم چسبیده و یا به صورت جداگانه در آشپزخانه قرار گیرند. این ویژگی انعطاف‌پذیری بالایی را برای چیدمان در آشپزخانه‌های با ابعاد مختلف فراهم می‌کند.\r\n\r\nسیستم گردش هوای فراگیر در این مدل باعث می‌شود که سرما به صورت یکنواخت در تمام طبقات پخش شود؛ بنابراین مواد غذایی در طبقات بالا و پایین تازگی خود را به یک اندازه حفظ می‌کنند. قابلیت نوفراست (No-Frost) یا همان بدون برفک در هر دو کابین یخچال و فریزر وجود دارد، بنابراین دیگر نیازی به خاموش کردن دستگاه و دردسرهای آب کردن برفک‌ها نخواهید داشت. نمایشگر دیجیتال روی درب به شما امکان می‌دهد دمای دقیق هر بخش را مشاهده و تنظیم کنید و در صورت باز ماندن درب، سیستم اخطار صوتی شما را مطلع می‌کند.\r\n\r\nطبقات شیشه‌ای نشکن، کشوهای مخصوص میوه و سبزیجات با قابلیت حفظ رطوبت و کشوهای جادار در بخش فریزر، نظم‌دهی به مواد غذایی را بسیار آسان کرده است. موتور کم‌مصرف و عایق‌بندی استاندارد بدنه باعث شده تا این محصول رتبه انرژی مناسبی داشته باشد و هزینه‌های برق خانوار را کاهش دهد. اگر به دنبال محصولی جادار، با دوام و با خدمات پس از فروش گسترده در سراسر کشور هستید، دوقلوی پارس انتخابی هوشمندانه است.", "fridge.jpg", 2, false, 22000000m, "یخچال فریزر دوقلو پارس" },
                    { 5, 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "کتاب Clean Code (کدنویسی تمیز) اثر رابرت سی. مارتین که در دنیای برنامه‌نویسی با نام «عمو باب» شناخته می‌شود، بیش از آنکه یک کتاب آموزشی باشد، یک مانیفست و راهنمای اخلاقی برای توسعه‌دهندگان نرم‌افزار است. این کتاب با این پیش‌فرض شروع می‌شود که «کد نوشته می‌شود تا توسط انسان‌ها خوانده شود، نه فقط برای اجرا توسط ماشین». اگر شما کدی می‌نویسید که کار می‌کند اما خواندن و درک آن برای هم‌تیمی‌هایتان (یا حتی خودتان پس از ۶ ماه) دشوار است، این کتاب دقیقاً برای شماست.\r\n\r\nرابرت مارتین در این اثر جاودانه، اصول SOLID، نام‌گذاری صحیح متغیرها و توابع، ساختار کلاس‌ها و نحوه نوشتن کامنت‌های مفید (و پرهیز از کامنت‌های بد) را با مثال‌های عملی فراوان توضیح می‌دهد. او به شما می‌آموزد که چگونه کدهای «بد بو» (Code Smells) را شناسایی کرده و آن‌ها را بازنویسی (Refactor) کنید. فصل‌های مربوط به مدیریت خطا (Error Handling) و تست‌نویسی (Unit Testing) دیدگاه شما را نسبت به کیفیت نرم‌افزار کاملاً تغییر خواهد داد.\r\n\r\nخواندن این کتاب برای هر برنامه‌نویسی در هر سطحی (از جونیور تا ارشد) واجب است. این کتاب به شما یاد می‌دهد که تفاوت بین یک «کدنویس» معمولی و یک «مهندس نرم‌افزار حرفه‌ای» در جزئیات و تمیزی کار نهفته است. ترجمه روان و مثال‌های جاوا (که به راحتی قابل تعمیم به C# و سایر زبان‌های شی‌گرا است) باعث شده تا این کتاب به عنوان مرجع اصلی در بسیاری از شرکت‌های نرم‌افزاری بزرگ دنیا تدریس شود. با خواندن Clean Code، شما سرمایه‌گذاری بزرگی روی آینده شغلی و اعتبار حرفه‌ای خود انجام می‌دهید.", "clean_code_book.jpg", 100, false, 450000m, "کتاب Clean Code" },
                    { 6, 5, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "سفر به دل طبیعت و کمپینگ بدون داشتن یک سرپناه امن و راحت، لطفی ندارد. چادر مسافرتی 4 نفره مدل فنری، گزینه‌ای عالی برای خانواده‌ها و گروه‌های دوستانه‌ای است که می‌خواهند آخر هفته‌ای آرام را در جنگل، کویر یا کنار دریا سپری کنند. مهم‌ترین ویژگی این چادر، مکانیزم باز و بسته شدن فنری آن است؛ به طوری که تنها در چند ثانیه برپا می‌شود و جمع کردن آن نیز بسیار ساده است، بنابراین وقت شما برای درگیری با میله‌ها و طناب‌ها تلف نمی‌شود.\r\n\r\nپارچه استفاده شده در این محصول از جنس پلی‌استر با روکش ضد آب است که شما را در برابر باران‌های ناگهانی و رطوبت زمین محافظت می‌کند. در عین حال، پنجره‌های توری‌دار بزرگ تعبیه شده در طرفین، تهویه هوای مطبوعی را ایجاد می‌کنند و با داشتن پشه‌بند، از ورود حشرات مزاحم به داخل چادر جلوگیری می‌کنند. کف چادر از پارچه‌ای ضخیم‌تر ساخته شده تا در برابر سایش سنگریزه و نفوذ رطوبت از کف مقاوم باشد.\r\n\r\nفضای داخلی این چادر برای خواب راحت 4 نفر بزرگسال طراحی شده است و ارتفاع سقف آن به گونه‌ای است که نشستن در آن راحت باشد. کیف حمل دایره‌ای شکل و وزن سبک آن باعث می‌شود تا حمل و نقل چادر در صندوق عقب خودرو یا حتی به صورت دستی بسیار آسان باشد. دوخت‌های آپارات شده و زیپ‌های باکیفیت، طول عمر این محصول را تضمین می‌کنند تا سال‌ها همسفر مطمئن شما در ماجراجویی‌هایتان باشد.", "camping_tent.jpg", 10, false, 1800000m, "چادر مسافرتی 4 نفره" },
                    { 7, 5, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "یک کوهنورد حرفه‌ای می‌داند که کوله‌پشتی مهم‌ترین یار او در مسیرهای سخت است. کوله پشتی کوهنوردی 50 لیتری، با مهندسی ارگونومیک دقیق طراحی شده تا وزن سنگین تجهیزات را به جای شانه‌ها، روی لگن و پاها تقسیم کند. این ویژگی باعث می‌شود که حتی در پیمایش‌های طولانی و چند روزه، احساس خستگی کمتری داشته باشید و از آسیب به ستون فقرات جلوگیری شود. سیستم پشتی دارای توری عرق‌گیر است که با ایجاد فاصله بین کوله و کمر، جریان هوا را برقرار کرده و از تعریق بیش از حد جلوگیری می‌کند.\r\n\r\nظرفیت 50 لیتری این کوله فضای کافی برای کیسه خواب، لباس‌های اضافی، ظروف پخت‌وپز و آب و غذا را فراهم می‌کند. جیب‌های متعدد زیپ‌دار و کشسانی در طرفین و روی کلاهک کوله، دسترسی سریع به لوازمی مثل نقشه، قطب‌نما، بطری آب و تنقلات را ممکن می‌سازد. همچنین بندهای مخصوصی برای حمل زیرانداز، باتوم کوهنوردی و کلنگ یخ روی بدنه تعبیه شده است.\r\n\r\nجنس پارچه این کوله از نوع برزنت ضد خش و مقاوم در برابر پارگی است که در برخورد با صخره‌ها و شاخ و برگ درختان آسیب نمی‌بیند. علاوه بر این، یک کاور ضد باران (Rain Cover) در جیب مخفی پایین کوله وجود دارد که در شرایط طوفانی می‌توانید آن را روی کل کوله بکشید تا وسایلتان کاملاً خشک بمانند. سگک‌های نشکن و زیپ‌های روان YKK از دیگر ویژگی‌هایی هستند که کیفیت ساخت بالای این محصول را تضمین می‌کنند.", "backpack.jpg", 8, false, 3500000m, "کوله پشتی کوهنوردی 50 لیتری" },
                    { 8, 1, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "ساعت هوشمند سامسونگ Galaxy Watch 6، ترکیبی بی‌نظیر از زیبایی شناسی کلاسیک و تکنولوژی مدرن پوشیدنی است که برای پایش سلامتی و تناسب اندام شما طراحی شده است. این ساعت با حاشیه‌های باریک‌تر و صفحه‌نمایش بزرگ‌تر و شفاف‌تر از نسل قبل، تعامل با رابط کاربری را بسیار لذت‌بخش کرده است. کریستال سافایر به کار رفته در نمایشگر، مقاومت فوق‌العاده‌ای در برابر خط و خش ایجاد می‌کند تا در فعالیت‌های روزمره و ورزشی نگران آسیب دیدن آن نباشید.\r\n\r\nمهم‌ترین تمرکز سامسونگ در این مدل، بهبود خواب کاربر است. سنسورهای پیشرفته با پایش مراحل مختلف خواب و ارائه آنالیز دقیق (Sleep Coaching)، به شما کمک می‌کنند عادات خواب خود را اصلاح کنید و روز پرانرژی‌تری داشته باشید. سنسور BioActive سامسونگ نیز ترکیبی از سه حسگر قدرتمند است که ضربان قلب، نوار قلب (ECG) و تحلیل امپدانس بیوالکتریک (BIA) را اندازه‌گیری می‌کند؛ بدین ترتیب شما می‌توانید درصد چربی بدن، توده عضلانی و آب بدن خود را در هر لحظه بسنجید.\r\n\r\nقابلیت‌های ارتباطی این ساعت فراتر از انتظار است؛ امکان مکالمه مستقیم، پاسخ به پیام‌ها، و کنترل دوربین گوشی از راه دور تنها بخشی از امکانات آن هستند. باتری بهبود یافته با قابلیت شارژ سریع به شما اجازه می‌دهد با تنها 8 دقیقه شارژ، تا 8 ساعت از قابلیت پایش خواب استفاده کنید. این ساعت با طراحی شیک و بندهای قابل تعویض (One-Click)، هم برای جلسات رسمی و هم برای باشگاه ورزشی انتخابی ایده‌آل است.", "galaxy_watch6.jpg", 15, false, 12500000m, "ساعت هوشمند سامسونگ Galaxy Watch 6" },
                    { 9, 3, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "ماشین لباسشویی 9 کیلویی ال‌جی با بهره‌گیری از تکنولوژی هوش مصنوعی (AI DD)، فراتر از یک شستشوی ساده عمل می‌کند. این فناوری نه تنها وزن لباس‌ها را تشخیص می‌دهد، بلکه جنس پارچه را نیز شناسایی کرده و بر اساس آن، الگوی حرکت دیگ را انتخاب می‌کند. این ویژگی باعث می‌شود تا 18 درصد محافظت بیشتری از الیاف پارچه صورت گیرد و طول عمر لباس‌های مورد علاقه شما افزایش یابد.\r\n\r\nموتور Inverter Direct Drive در این محصول، با حذف تسمه و قطعات مکانیکی اضافی، لرزش و صدای دستگاه را به حداقل رسانده است، به طوری که حتی در هنگام خشک‌کن با دور بالا نیز آرامش خانه حفظ می‌شود. تکنولوژی بخارشوی (Steam+) یکی دیگر از ویژگی‌های کلیدی است که با نفوذ بخار به بافت لباس، 99.9 درصد از آلرژن‌ها و باکتری‌ها را از بین می‌برد و در عین حال چروک‌های لباس را تا 30 درصد کاهش می‌دهد، که این امر اتوکشی را بسیار آسان‌تر می‌کند.\r\n\r\nطراحی درب شیشه‌ای حرارت‌دیده و مخزن استیل ضد زنگ با خاصیت ضدباکتریایی، نشان‌دهنده کیفیت ساخت بالای این محصول است. قابلیت اضافه کردن لباس در حین شستشو (Add Item) برای زمان‌هایی که لباسی را فراموش کرده‌اید بسیار کاربردی است. همچنین با استفاده از سیستم عیب‌یابی هوشمند و اتصال به موبایل از طریق Wi-Fi، می‌توانید برنامه‌های شستشوی جدید را دانلود کرده و وضعیت دستگاه را از راه دور کنترل کنید.", "lg_washing_machine.jpg", 3, false, 32000000m, "ماشین لباسشویی 9 کیلویی ال‌جی" },
                    { 10, 2, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "این هودی مردانه با طراحی مینیمال و ساده، گزینه‌ای اساسی برای کمد لباس هر فرد شیک‌پوشی در فصول سرد سال است. پارچه استفاده شده در این محصول از نوع دورس سه نخ پنبه‌ای با کیفیت اعلاء است که داخل آن کرک‌دار (Brush) شده است. این ویژگی باعث می‌شود که هودی علاوه بر نرمی فوق‌العاده، گرمای بدن را به خوبی حفظ کند و برای روزهای پاییزی و زمستانی انتخابی بی‌نظیر باشد.\r\n\r\nدوخت‌های این محصول با دقت و ظرافت بالا انجام شده و در قسمت سرآستین‌ها و پایین لباس از کشبافت مقاوم استفاده شده تا از ورود سرما جلوگیری شود و فرم لباس در اثر شستشوهای مکرر تغییر نکند. کلاه هودی به صورت دولایه دوخته شده تا ایستایی زیبایی داشته باشد و بندهای تنظیم‌کننده آن دارای سر‌های فلزی هستند که به دوام و زیبایی کار می‌افزایند. جیب کانگورویی در جلوی لباس، فضای مناسبی برای گرم کردن دست‌ها یا قرار دادن گوشی و کلید فراهم می‌کند.\r\n\r\nرنگ‌بندی این هودی با استفاده از رنگ‌های ری‌اکتیو انجام شده که ثبات بسیار بالایی دارند و پس از شستشو دچار بور شدگی نمی‌شوند. قواره لباس به صورت آزاد (Relaxed Fit) طراحی شده که راحتی حرکت را تضمین می‌کند و به شما اجازه می‌دهد آن را به راحتی روی تیشرت یا زیر کاپشن و پالتو بپوشید. این هودی برای استایل‌های خیابانی (Street Style)، ورزش و استفاده روزمره در منزل کاملاً مناسب است.", "hoodie_men.jpg", 50, false, 980000m, "هودی مردانه طرح مینیمال" },
                    { 11, 4, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "کتاب C# in Depth اثر جان اسیت (Jon Skeet)، یکی از افسانه‌ای‌ترین چهره‌های دنیای دات‌نت و کاربران Stack Overflow، کتابی است که نگاه شما را به زبان سی‌شارپ عمیق‌تر می‌کند. اگر تصور می‌کنید سی‌شارپ را بلد هستید، این کتاب به شما نشان می‌دهد که چقدر هنوز برای یادگیری وجود دارد. این اثر برای مبتدیان نیست، بلکه برای توسعه‌دهندگانی است که می‌خواهند از سطح متوسط به سطح پیشرفته و حرفه‌ای ارتقا پیدا کنند.\r\n\r\nجان اسیت در این کتاب با بیانی دقیق و موشکافانه، تکامل زبان سی‌شارپ را از نسخه 2 تا جدیدترین نسخه‌ها بررسی می‌کند. او فقط به آموزش سینتکس نمی‌پردازد، بلکه «چرا»ها و «چگونه»های پشت پرده کامپایلر را توضیح می‌دهد. مباحثی مانند Async/Await، Expression Trees، Dynamic Typing و ویژگی‌های جدید Functional Programming در سی‌شارپ، با مثال‌هایی تشریح شده‌اند که ذهنیت شما را نسبت به کدنویسی تغییر می‌دهند.\r\n\r\nیکی از نقاط قوت کتاب، تمرکز بر دام‌هایی است که برنامه‌نویسان اغلب در آن‌ها گرفتار می‌شوند (Common Pitfalls). خواندن این کتاب به شما اعتماد به نفس می‌دهد تا از ویژگی‌های پیچیده زبان با اطمینان استفاده کنید و کدهایی بنویسید که نه تنها کار می‌کنند، بلکه بهینه، تمیز و اصولی هستند. این کتاب پلی است بین یک کدنویس معمولی و یک مهندس نرم‌افزار که بر ابزار کار خود تسلط کامل دارد.", "csharp_in_depth.jpg", 25, false, 380000m, "کتاب C# in Depth (ویرایش چهارم)" },
                    { 12, 5, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "خواب راحت و گرم در دل طبیعت وحشی و دمای زیر صفر، حیاتی‌ترین نیاز یک کوهنورد است. کیسه خواب پر (Down) با دمای کامفورت منفی 10 درجه، طراحی شده است تا در شرایط سخت زمستانی محافظ جان شما باشد. عایق داخلی این کیسه خواب از پر غاز با تراکم (Fill Power) 800 تشکیل شده که بهترین نسبت گرما به وزن را در میان عایق‌ها ارائه می‌دهد؛ به این معنی که با کمترین وزن و حجم، بیشترین گرما را تولید می‌کند.\r\n\r\nپارچه بیرونی از نایلون Ripstop ضد آب و ضد باد ساخته شده که در برابر پارگی بسیار مقاوم است و اجازه نفوذ رطوبت شبنم یا برف آب شده را به داخل نمی‌دهد. طراحی مومیایی شکل (Mummy) کیسه خواب باعث می‌شود فضای خالی اطراف بدن کاهش یابد و گرمای بدن سریع‌تر محیط داخلی را گرم کند. همچنین در قسمت پاها (Footbox) فضای اضافی در نظر گرفته شده تا پاها در حالت طبیعی و راحت قرار گیرند و گردش خون مختل نشود.\r\n\r\nیکی از ویژگی‌های بارز این محصول، زیپ‌های YKK دوطرفه است که امکان تهویه هوا در صورت گرم شدن بیش از حد را فراهم می‌کند. کلاه کیسه خواب دارای بندهای تنظیم شونده است که کاملاً دور صورت کیپ می‌شود و از هدر رفتن گرما از ناحیه سر جلوگیری می‌کند. این محصول همراه با یک کیسه استراحت (برای نگهداری در منزل) و یک کیسه فشرده‌سازی (Compression Sack) عرضه می‌شود که حجم آن را برای قرارگیری در کوله‌پشتی به اندازه یک توپ فوتبال کوچک می‌کند.", "sleeping_bag.jpg", 6, false, 5500000m, "کیسه خواب پر کوهنوردی" },
                    { 13, 1, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "آیپد ایر نسل 5 با بهره‌گیری از تراشه قدرتمند M1 (همان تراشه‌ای که در مک‌بوک‌های پرو استفاده شده)، مرز بین تبلت و لپ‌تاپ را از بین برده است. این دستگاه با قدرتی خیره‌کننده، امکان ویرایش ویدیوهای 4K، طراحی‌های سه‌بعدی سنگین و اجرای بازی‌های گرافیکی سطح کنسول را به راحتی فراهم می‌کند. با این حال، همچنان وزن بسیار سبک و ضخامت کمی دارد که حمل آن را در هر کیف دستی ممکن می‌سازد.\r\n\r\nصفحه‌نمایش 10.9 اینچی Liquid Retina با تکنولوژی True Tone و پوشش ضد بازتاب نور، تصاویری با رنگ‌های زنده و واقعی نمایش می‌دهد که برای طراحان گرافیک و هنرمندان دیجیتال یک بوم نقاشی ایده‌آل است. پشتیبانی از قلم اپل نسل 2 (Apple Pencil 2) که به صورت مغناطیسی به بدنه می‌چسبد و شارژ می‌شود، تجربه نوشتن و طراحی را به قدری روان و دقیق کرده که حس کار با کاغذ و قلم واقعی را تداعی می‌کند.\r\n\r\nدوربین سلفی 12 مگاپیکسلی اولترا واید با قابلیت Center Stage، به صورت هوشمند شما را در حین تماس‌های ویدیویی دنبال می‌کند تا همیشه در مرکز کادر باشید. درگاه USB-C در این نسل سرعت انتقال داده دو برابری دارد و امکان اتصال مستقیم به درایوهای اکسترنال، دوربین‌ها و نمایشگرهای بزرگ را فراهم می‌کند. حسگر اثر انگشت Touch ID ادغام شده با دکمه پاور، امنیت دستگاه را تضمین کرده و دسترسی سریع را ممکن می‌سازد.", "ipad_air5.jpg", 8, false, 29000000m, "تبلت اپل iPad Air 5" },
                    { 14, 3, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "جاروبرقی بوش سری 8، شاهکار مهندسی آلمان، ترکیبی از قدرت مکش فوق‌العاده و عملکرد بی‌صدا است. سیستم QuattroPower در این دستگاه تضمین می‌کند که با کمترین مصرف انرژی، بیشترین کارایی تمیزکنندگی حاصل شود. موتور پیشرفته این دستگاه با گارانتی 10 ساله، خیال شما را از بابت طول عمر و دوام آن کاملاً راحت می‌کند. این جاروبرقی حتی ریزترین ذرات گرد و غبار را که در عمق تار و پود فرش نفوذ کرده‌اند، بیرون می‌کشد.\r\n\r\nیکی از مهم‌ترین ویژگی‌های این مدل برای افراد مبتلا به آلرژی، سیستم فیلتراسیون HEPA قابل شستشو است. این فیلتر 99.95 درصد از ذرات ریز، باکتری‌ها و آلاینده‌ها را جذب می‌کند و هوایی که از دستگاه خارج می‌شود، تمیزتر از هوای محیط اتاق است. کیسه گرد و غبار با ظرفیت XXL (5 لیتر) نیاز به تعویض مکرر را از بین می‌برد و با طراحی چند لایه، حتی هنگام تعویض نیز گرد و خاک را در خود حبس می‌کند.\r\n\r\nلوله تلسکوپی و خرطومی کنفی با بافت بسیار مقاوم، در برابر کشیدگی و پارگی مقاوم هستند. سری‌های متنوع همراه دستگاه، تمیز کردن درز مبل‌ها، پرده‌ها و گوشه‌های غیرقابل دسترس را آسان می‌کنند. سیستم PowerProtect تضمین می‌کند که حتی با پر شدن کیسه، مکش دستگاه افت نکند. حرکت روان چرخ‌های فلزی با قابلیت چرخش 360 درجه، جابجایی دستگاه روی انواع سطوح سرامیک و فرش را بدون آسیب رساندن به آن‌ها ممکن می‌سازد.", "bosch_vacuum.jpg", 4, false, 14500000m, "جاروبرقی بوش سری 8" },
                    { 15, 2, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "شلوار جین راسته (Regular Fit) کلاسیک، آیتمی است که هرگز از مد نمی‌افتد و پایه ثابت استایل آقایان شیک‌پوش است. این شلوار از پارچه دنیم (Denim) صد درصد پنبه‌ای با گرماژ بالا تهیه شده است که دوام و مقاومت بسیار بالایی در برابر ساییدگی و پارگی دارد. برخلاف شلوارهای جین نازک و کشی، این مدل ایستایی بسیار محکم و مردانه‌ای دارد و با گذشت زمان و استفاده مداوم، فرم خود را حفظ کرده و حتی زیباتر می‌شود (اصطلاحاً جا باز می‌کند).\r\n\r\nرنگ سرمه‌ای تیره (Dark Wash) این شلوار با فرآیندهای سنگ‌شور صنعتی دقیق به دست آمده که ظاهری اصیل و باوقار به آن بخشیده است. این رنگ به راحتی با انواع پیراهن‌های سفید، تیشرت‌های رنگی و حتی کت‌های اسپرت ست می‌شود و هم برای محیط‌های نیمه‌رسمی و هم برای دورهمی‌های دوستانه مناسب است. دوخت‌های دو سوزنه با نخ ضخیم نارنجی رنگ، علاوه بر استحکام درزها، جلوه‌ای کلاسیک و جذاب به کار داده است.\r\n\r\nدکمه‌ها و پرچ‌های فلزی به کار رفته از آلیاژ ضد زنگ هستند و زیپ روان و باکیفیت YKK استفاده شده در آن، سال‌ها بدون مشکل کار خواهد کرد. این شلوار دارای 5 جیب استاندارد (شامل جیب کوچک سکه) است. الگوی برش آن به گونه‌ای است که در قسمت ران و زانو آزادی عمل کافی را فراهم می‌کند و برای استفاده طولانی مدت در طول روز کاملاً راحت است.", "jeans_pant.jpg", 60, false, 1200000m, "شلوار جین راسته مردانه" },
                    { 16, 5, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "در کوهنوردی شبانه، غارنوردی یا کمپینگ، داشتن نور مناسب و دست‌های آزاد، کلید ایمنی و موفقیت است. چراغ پیشانی (هدلامپ) پتزل مدل Actik Core با قدرت روشنایی 450 لومن، تاریک‌ترین مسیرها را برای شما مثل روز روشن می‌کند. این مدل دارای پرتو نور ترکیبی (Wide و Focused) است؛ پرتو عریض برای دیدن محیط اطراف در کمپ و پرتو متمرکز برای دیدن فواصل دور در هنگام حرکت طراحی شده است.\r\n\r\nویژگی انقلابی این محصول تکنولوژی Hybrid Concept است؛ به این معنی که هم با باتری شارژی لیتیومی (Core) که در جعبه موجود است کار می‌کند و هم در شرایط اضطراری می‌توانید از سه عدد باتری نیم‌قلمی معمولی استفاده کنید. این قابلیت در سفرهای طولانی که دسترسی به برق برای شارژ وجود ندارد، بسیار حیاتی است. نور قرمز تعبیه شده در این هدلامپ برای دید در شب بدون خیره کردن چشم هم‌نوردان و یا خواندن نقشه در تاریکی کاربرد دارد و حالت چشمک‌زن آن می‌تواند در شرایط اضطراری برای درخواست کمک از فواصل دور (تا 700 متر) دیده شود.\r\n\r\nاستاندارد ضد آب IPX4 باعث می‌شود که بتوانید زیر بارش باران و برف با خیال راحت از آن استفاده کنید. بند پیشانی آن دارای خاصیت بازتاب‌دهنده نور است که باعث می‌شود در تاریکی دیده شوید و سوت نجات تعبیه شده روی بند، برای مواقع خطر بسیار کارآمد است. با وجود تمام این امکانات، وزن این هدلامپ تنها 75 گرم است و هیچگونه سنگینی روی پیشانی احساس نخواهید کرد.", "headlamp.jpg", 12, false, 1600000m, "چراغ پیشانی (هدلامپ) پتزل" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityUserId",
                table: "Users",
                column: "IdentityUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
