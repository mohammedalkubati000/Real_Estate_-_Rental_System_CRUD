# 🏢 نظام إدارة العقارات والإيجارات المتكامل (Real Estate & Rental System)

دليل برمي وشرح تفصيلي شامل لبناء وتطبيق نظام إدارة عقارات متكامل من الصفر المطلق باستخدام تقنية **ASP.NET Core MVC** وقاعدة بيانات **Azure SQL Database** مع تطبيق العلاقات المركبة وتعبئة البيانات تلقائياً.

---

## 🛠️ التقنيات والمكتبات المستخدمة (Tech Stack)
* **Framework:** .NET 8.0 / 9.0 (ASP.NET Core MVC)
* **ORM:** Entity Framework Core
* **Database:** Azure SQL Database
* **Database Tool:** Azure Data Studio / SSMS
* **Frontend:** Razor Views, Bootstrap 5, HTML5, CSS3

---

## 🚀 مراحل بناء وتطبيق المشروع خطوة بخطوة (Step-by-Step Guide)

### 💻 المرحلة 1: إنشاء المشروع وتهيئته البرمجية
1. **تشغيل بيئة العمل:** نقوم بفتح برنامج **Visual Studio 2022**.
2. **نوع القالب:** نختار **Create a new project**، وفي خانة البحث نكتب `MVC` ثم نختار قالب **ASP.NET Core Web App (Model-View-Controller)** ونضغط **Next**.
3. **تسمية المشروع والموقع:** 
   * في خانة **Project name** نكتب الاسم بدقة: `Real_Estate_&_Rental_System_CRUD`.
   * نحدد مجلد الحفظ ثم نضغط **Next**، ونختار إصدار **.NET** المعتمد لدينا ثم نضغط **Create**.
4. **تثبيت حزم ترحيل البيانات:** من القائمة العلوية نفتح الـ `Package Manager Console` وننفذ الأوامر التالية بالترتيب لتثبيت الأدوات:
   ```bash
   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   Install-Package Microsoft.EntityFrameworkCore.Tools
   ```

---

### 📁 المرحلة 2: بناء هيكل الجداول والمودلز (Models)
داخل مجلد **`Models`** الافتراضي، قمنا بإنشاء 4 كلاسات لتمثيل جداول النظام مع تفعيل شروط التحقق والترجمة العربية للحقول:

1. **كلاس العقارات (`Property.cs`):** يحفظ نوع العقار، المدينة، العنوان، وقيمة الإيجار السنوي وحالة الإتاحة.
2. **كلاس المستأجرين (`Tenant.cs`):** يحفظ بيانات الأشخاص كاملة (الاسم، الهوية، الجوال، والبريد).
3. **كلاس عقود الإيجار (`RentalContract.cs`):** يربط العقار بالمستأجر بروابط ثنائية (`Foreign Keys`) مع تحديد تواريخ العقد والقيمة المتفق عليها.
4. **كلاس سجل المدفوعات (`Payment.cs`):** يتتبع الأقساط والمدفوعات التابعة لكل عقد، مع تحديد طريقة الدفع وحالة الدفعة.

---

### 🗄️ المرحلة 3: إعداد الـ DbContext وترحيل الجداول للسيرفر
1. **إنشاء مجلد الاتصال:** نقوم بإنشاء مجلد فرعي جديد باسم **`Data`**، وبداخله ننشئ كلاس **`AppDbContext.cs`** ليرث من `DbContext` ونعرف فيه أسطر الـ `DbSet` للأربعة جداول.
2. **نص الاتصال:** نضع البيانات الخاصة بسيرفر الـ Azure SQL داخل ملف `appsettings.json`.
3. **الربط والتشغيل:** نربط قاعدة البيانات في ملف `Program.cs` عبر دالة `AddDbContext`.
4. **بناء الجداول في Azure:** نفتح الـ `Package Manager Console` ونرسل الجداول إلى السيرفر الفعلي عبر الأوامر:
   ```bash
   Add-Migration InitialCreateRealEstate
   Update-Database
   ```

---

### 🎮 المرحلة 4: برمجة الـ Controllers والـ Views للعمليات (CRUD)

1. **برمجة الـ Controllers:** قمنا بإنشاء الكنترولرات الأربعة بالاعتماد على الأسلوب المتزامن الصافي والنظيف مستخدمين الـ `_db` والـ `ToList()` لتسهيل وسرعة قراءة البيانات.
2. **تصميم الواجهات المخصصة (Razor Views):** قمنا بإنشاء مجلدات فرعية داخل مجلد `Views` تحمل الأسماء المتطابقة مع الكنترولرات، وبداخل كل مجلد قمنا بإضافة أربعة ملفات واجهات أساسية تنتهي بـ `.cshtml` وهي (`Index`, `Create`, `Edit`, `Delete`) منسقة بالكامل بكلاسات الـ Bootstrap الجاهزة.
3. **تفعيل القوائم المنسدلة الذكية (Dropdowns):**
   * في شاشة إضافة عقد إيجار: يظهر للمستخدم قائمتان منسدلتان لتحديد العقار المتاح والمستأجر عبر الـ `ViewBag`.
   * في شاشة تسجيل دفعة مالية: تظهر قائمة منسدلة تدمج رقم العقد مع اسم المستأجر لتسهيل الحفظ والربط السليم.
4. **توجيه المسارات الصريحة:** تم تضمين توجيه المسار الصريح للملفات للتأكد من قراءة محرك الـ MVC للملفات مباشرة بدون تعليق.
5. **تحديث الـ Navbar الرئيسي:** فتحنا ملف `_Layout.cshtml` ووضعنا الروابط الأربعة لسهولة التنقل الفوري بين أقسام النظام من القائمة العلوية للموقع.

---

### 🚀 المرحلة 5: التعبئة التلقائية الذكية لـ 50 سجل متكامل (Data Seeding)
لتجربة النظام بشكل واقعي دون تضييع الوقت في الكتابة اليدوية:
1. اتصلنا بقاعدة البيانات المستضافة عبر برنامج **Azure Data Studio**.
2. فتحنا نافذة استعلام جديدة **`New Query`** وقمنا بتشغيل سكربت SQL احترافي يعتمد على الـ `CURSOR` والـ `SCOPE_IDENTITY()`.
3. السكربت قام بتصفير الجداول، ثم زرع 50 عقاراً متنوعاً بالمدن والسعر، و50 مستأجراً ببيانات واقعية، وقام بإنشاء عقود إيجار ودفعات مالية منزلة بحساباتهم آلياً مع دعم الحروف العربية باستخدام البادئة `N'`.

---

## 📤 رسالة الـ Commit الرسمية المعتمدة للرفع (GitHub Commit Message)

عند الانتهاء من مراجعة البناء وظهور عبارة **Build Succeeded مع 0 Errors**، تم رفع المشروع إلى مستودع GitHub باستخدام رسالة الـ Commit الاحترافية الموقعة بالتالي:

```text
Chore: Rebuild system architecture, fix MVC directory sync, and seed 50 real estate records

Successfully expanded the platform into a multi-table Real Estate & Rental System with the following implementations:
1. Multi-Table Infrastructure: Configured 4 database entities ('Property', 'Tenant', 'RentalContract', and 'Payment') to map real estate business workflows securely.
2. Clean View-Controller Synchronization: Fixed the directory cache collision by renaming the View folder to match 'RentalContractsController' and overriding explicit view paths.
3. Dropdown Bindings: Interlinked complex entities across forms (Properties and Tenants inside Contracts; Contracts inside Payments) utilizing smart ViewBags for intuitive HTML selection.
4. Database Seeding: Created a transactional SQL script leveraging cursors and SCOPE_IDENTITY() to dynamically populate the Azure Server with 50 unique properties, 50 tenants, automated rental contracts, and accurate down-payments.

Date: Thursday, September 17, 2026
via Mohammed Alkubati
```

---
Developed and Documented **via Mohammed Alkubati** © 2026
