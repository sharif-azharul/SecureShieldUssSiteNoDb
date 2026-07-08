using SecureShield.Domain.Entities;

namespace SecureShield.Infrastructure.Data;

/// <summary>
/// In-memory static data store — replaces the database.
/// All data lives in static collections that persist for the lifetime of the app process.
/// Career applications and contact messages submitted via forms are stored in memory
/// and visible in the admin panel until the app restarts.
/// </summary>
public static class StaticData
{
    // Thread-safe collections for runtime-submitted data
    public static readonly List<CareerApplication> CareerApplications = new();
    public static readonly List<ContactMessage> ContactMessages = new();
    public static readonly object Lock = new();

    // ---------- SERVICES (16) ----------
    public static readonly List<Service> Services = new()
    {
        new Service { Slug = "industrial", Icon = "Factory", Featured = true, Order = 1,
            TitleEn = "Industrial Security", TitleBn = "শিল্প নিরাপত্তা",
            DescriptionEn = "Round-the-clock guarding for factories and industrial plants with access control, patrolling and incident reporting.",
            DescriptionBn = "কারখানা ও শিল্প প্লান্টের জন্য অ্যাক্সেস কন্ট্রোল, প্যাট্রোলিং ও ঘটনা রিপোর্টিং সহ চব্বিশ ঘন্টা পাহারা।" },
        new Service { Slug = "corporate", Icon = "Building2", Featured = true, Order = 2,
            TitleEn = "Corporate Security", TitleBn = "কর্পোরেট নিরাপত্তা",
            DescriptionEn = "Professional front-desk and corporate office security aligned with your brand image and visitor policy.",
            DescriptionBn = "আপনার ব্র্যান্ড ইমেজ ও ভিজিটর নীতি অনুযায়ী পেশাদার ফ্রন্ট-ডেস্ক এবং কর্পোরেট অফিস নিরাপত্তা।" },
        new Service { Slug = "bank", Icon = "Landmark", Featured = true, Order = 3,
            TitleEn = "Bank Security", TitleBn = "ব্যাংক নিরাপত্তা",
            DescriptionEn = "Trained armed guards for banks, ATMs and cash-in-transit operations with strict SOP compliance.",
            DescriptionBn = "কঠোর SOP মেনে ব্যাংক, এটিএম ও ক্যাশ-ইন-ট্রানজিট অপারেশনের জন্য প্রশিক্ষিত সশস্ত্র গার্ড।" },
        new Service { Slug = "hospital", Icon = "Hospital", Featured = true, Order = 4,
            TitleEn = "Hospital Security", TitleBn = "হাসপাতাল নিরাপত্তা",
            DescriptionEn = "Sensitive crowd control, ICU access management and patient-staff protection for healthcare facilities.",
            DescriptionBn = "স্বাস্থ্যসেবা সুবিধার জন্য সংবেদনশীল ভিড় নিয়ন্ত্রণ, আইসিইউ অ্যাক্সেস ব্যবস্থাপনা ও রোগী-কর্মী সুরক্ষা।" },
        new Service { Slug = "residential", Icon = "Home", Order = 5,
            TitleEn = "Residential Security", TitleBn = "আবাসিক নিরাপত্তা",
            DescriptionEn = "Gated community and apartment complex guards managing visitors, deliveries and perimeter safety.",
            DescriptionBn = "ভিজিটর, ডেলিভারি ও পরিসীমা নিরাপত্তা পরিচালনাকারী গেটেড কমিউনিটি ও অ্যাপার্টমেন্ট গার্ড।" },
        new Service { Slug = "mall", Icon = "ShoppingBag", Featured = true, Order = 6,
            TitleEn = "Shopping Mall Security", TitleBn = "শপিং মল নিরাপত্তা",
            DescriptionEn = "Customer-friendly mall guarding, loss prevention teams, parking patrol and emergency response.",
            DescriptionBn = "গ্রাহক-বান্ধব মল পাহারা, ক্ষতি প্রতিরোধ দল, পার্কিং প্যাট্রোল ও জরুরি প্রতিক্রিয়া।" },
        new Service { Slug = "warehouse", Icon = "Warehouse", Order = 7,
            TitleEn = "Warehouse Security", TitleBn = "গুদামঘর নিরাপত্তা",
            DescriptionEn = "Asset protection for distribution centers with inventory-patrol routines and gate logging.",
            DescriptionBn = "ইনভেন্টরি-প্যাট্রোল রুটিন ও গেট লগিং সহ ডিস্ট্রিবিউশন সেন্টারের জন্য সম্পদ সুরক্ষা।" },
        new Service { Slug = "factory", Icon = "Factory", Order = 8,
            TitleEn = "Factory Security", TitleBn = "কারখানা নিরাপত্তা",
            DescriptionEn = "Specialized guards for garments, textiles and manufacturing floors with worker-safety focus.",
            DescriptionBn = "শ্রমিক-নিরাপত্তার উপর ফোকাস সহ বস্ত্র, টেক্সটাইল ও উৎপাদন ফ্লোরের জন্য বিশেষায়িত গার্ড।" },
        new Service { Slug = "construction", Icon = "HardHat", Order = 9,
            TitleEn = "Construction Site Security", TitleBn = "নির্মাণস্থল নিরাপত্তা",
            DescriptionEn = "Material theft prevention, perimeter patrol and safety-zone enforcement on construction sites.",
            DescriptionBn = "নির্মাণস্থলে উপাদান চুরি প্রতিরোধ, পরিসীমা প্যাট্রোল ও সেফটি-জোন প্রয়োগ।" },
        new Service { Slug = "vip", Icon = "Crown", Featured = true, Order = 10,
            TitleEn = "VIP Protection", TitleBn = "ভিআইপি সুরক্ষা",
            DescriptionEn = "Discreet, professionally trained close-protection officers for executives, dignitaries and celebrities.",
            DescriptionBn = "নির্বাহী, গণ্যমান্য ব্যক্তি ও সেলিব্রিটিদের জন্য সুক্ষ্ম, পেশাদার প্রশিক্ষিত ক্লোজ-প্রটেকশন অফিসার।" },
        new Service { Slug = "bodyguard", Icon = "UserCheck", Order = 11,
            TitleEn = "Bodyguard Service", TitleBn = "বডিগার্ড সেবা",
            DescriptionEn = "Personal bodyguards for high-risk individuals and families — armed or unarmed per requirement.",
            DescriptionBn = "উচ্চ-ঝুঁকিপূর্ণ ব্যক্তি ও পরিবারের জন্য ব্যক্তিগত বডিগার্ড — প্রয়োজন অনুযায়ী সশস্ত্র বা নিরস্ত্র।" },
        new Service { Slug = "event", Icon = "Calendar", Order = 12,
            TitleEn = "Event Security", TitleBn = "ইভেন্ট নিরাপত্তা",
            DescriptionEn = "Stewards, crowd control and access management for concerts, weddings, conferences and sports events.",
            DescriptionBn = "কনসার্ট, বিবাহ, সম্মেলন ও ক্রীড়া ইভেন্টের জন্য স্টুয়ার্ড, ভিড় নিয়ন্ত্রণ ও অ্যাক্সেস ব্যবস্থাপনা।" },
        new Service { Slug = "reception", Icon = "ClipboardCheck", Order = 13,
            TitleEn = "Reception Security", TitleBn = "রিসেপশন নিরাপত্তা",
            DescriptionEn = "Bilingual reception-cum-security personnel for corporate lobbies, embassies and institutions.",
            DescriptionBn = "কর্পোরেট লবি, দূতাবাস ও প্রতিষ্ঠানের জন্য দ্বিভাষিক রিসেপশন-কাম-নিরাপত্তা কর্মী।" },
        new Service { Slug = "fire", Icon = "Flame", Order = 14,
            TitleEn = "Fire Safety Personnel", TitleBn = "অগ্নি নিরাপত্তা কর্মী",
            DescriptionEn = "Certified fire-safety officers stationed at factories, malls and high-rise buildings.",
            DescriptionBn = "কারখানা, মল ও উঁচু ভবনে নিযুক্ত সার্টিফায়েড অগ্নি নিরাপত্তা কর্মকর্তা।" },
        new Service { Slug = "cctv", Icon = "Cctv", Order = 15,
            TitleEn = "CCTV Monitoring", TitleBn = "সিসিটিভি মনিটরিং",
            DescriptionEn = "24/7 control-room operators monitoring client CCTV feeds with incident escalation protocols.",
            DescriptionBn = "ঘটনা এসকেলেশন প্রোটোকল সহ ক্লায়েন্ট সিসিটিভি ফিড মনিটরিং করে ২৪/৭ কন্ট্রোল-রুম অপারেটর।" },
        new Service { Slug = "consultancy", Icon = "Lightbulb", Order = 16,
            TitleEn = "Security Consultancy", TitleBn = "নিরাপত্তা পরামর্শ",
            DescriptionEn = "Risk assessments, SOP design, security audits and compliance consulting for organizations.",
            DescriptionBn = "প্রতিষ্ঠানের জন্য ঝুঁকি মূল্যায়ন, SOP ডিজাইন, নিরাপত্তা অডিট ও সম্মতি পরামর্শ।" },
    };

    // ---------- INDUSTRIES (12) ----------
    public static readonly List<Industry> Industries = new()
    {
        new Industry { Slug = "garments", Icon = "Shirt", TitleEn = "Garments", TitleBn = "তৈরি পোশাক", DescriptionEn = "BGMEA-member factories across Dhaka, Chattogram & Gazipur.", DescriptionBn = "ঢাকা, চট্টগ্রাম ও গাজীপুরের বিজিএমইএ-সদস্য কারখানা।" },
        new Industry { Slug = "banks", Icon = "Banknote", TitleEn = "Banks", TitleBn = "ব্যাংক", DescriptionEn = "Public, private & Islamic banks with nationwide branch networks.", DescriptionBn = "সারাদেশীয় শাখা নেটওয়ার্ক সহ সরকারি, বেসরকারি ও ইসলামিক ব্যাংক।" },
        new Industry { Slug = "hospitals", Icon = "Stethoscope", TitleEn = "Hospitals", TitleBn = "হাসপাতাল", DescriptionEn = "Multi-specialty hospitals, clinics and diagnostic chains.", DescriptionBn = "মাল্টি-স্পেশালিটি হাসপাতাল, ক্লিনিক ও ডায়াগনস্টিক চেইন।" },
        new Industry { Slug = "hotels", Icon = "Hotel", TitleEn = "Hotels", TitleBn = "হোটেল", DescriptionEn = "5-star city hotels, resorts and business hotels.", DescriptionBn = "৫-তারা সিটি হোটেল, রিসোর্ট ও বিজনেস হোটেল।" },
        new Industry { Slug = "corporate", Icon = "Building", TitleEn = "Corporate Offices", TitleBn = "কর্পোরেট অফিস", DescriptionEn = "Multinational and large local corporate headquarters.", DescriptionBn = "বহুজাতিক ও বড় স্থানীয় কর্পোরেট সদর দপ্তর।" },
        new Industry { Slug = "malls", Icon = "ShoppingBag", TitleEn = "Shopping Malls", TitleBn = "শপিং মল", DescriptionEn = "Large retail complexes and mixed-use developments.", DescriptionBn = "বড় খুচরা কমপ্লেক্স ও মিক্সড-ইউজ ডেভেলপমেন্ট।" },
        new Industry { Slug = "education", Icon = "GraduationCap", TitleEn = "Educational Institutions", TitleBn = "শিক্ষা প্রতিষ্ঠান", DescriptionEn = "Universities, colleges and international schools.", DescriptionBn = "বিশ্ববিদ্যালয়, কলেজ ও আন্তর্জাতিক স্কুল।" },
        new Industry { Slug = "factories", Icon = "Factory", TitleEn = "Factories", TitleBn = "কারখানা", DescriptionEn = "Heavy industry, FMCG, pharmaceuticals & electronics.", DescriptionBn = "ভারী শিল্প, এফএমসিজি, ফার্মাসিউটিক্যালস ও ইলেকট্রনিক্স।" },
        new Industry { Slug = "warehouses", Icon = "Warehouse", TitleEn = "Warehouses", TitleBn = "গুদামঘর", DescriptionEn = "3PL, e-commerce fulfillment & cold-storage facilities.", DescriptionBn = "থ্রিপিএল, ই-কমার্স ফুলফিলমেন্ট ও কোল্ড-স্টোরেজ সুবিধা।" },
        new Industry { Slug = "ngo", Icon = "HeartHandshake", TitleEn = "NGOs", TitleBn = "এনজিও", DescriptionEn = "International and national development organizations.", DescriptionBn = "আন্তর্জাতিক ও জাতীয় উন্নয়ন সংস্থা।" },
        new Industry { Slug = "govt", Icon = "Landmark", TitleEn = "Government Offices", TitleBn = "সরকারি অফিস", DescriptionEn = "Ministries, autonomous bodies and SOE facilities.", DescriptionBn = "মন্ত্রণালয়, স্বায়ত্তশাসিত সংস্থা ও এসওই সুবিধা।" },
        new Industry { Slug = "residential", Icon = "Home", TitleEn = "Residential Complexes", TitleBn = "আবাসিক কমপ্লেক্স", DescriptionEn = "Gated communities, high-rises and diplomat zones.", DescriptionBn = "গেটেড কমিউনিটি, হাই-রাইজ ও কূটনীতিক জোন।" },
    };

    // ---------- CLIENTS (12) ----------
    public static readonly List<Client> Clients = new()
    {
        new Client { Name = "Sonali Bank", Logo = "Sonali Bank", Order = 1 },
        new Client { Name = "BRAC Bank", Logo = "BRAC Bank", Order = 2 },
        new Client { Name = "Beximco", Logo = "BEXIMCO", Order = 3 },
        new Client { Name = "Square Group", Logo = "SQUARE", Order = 4 },
        new Client { Name = "Apex Group", Logo = "APEX", Order = 5 },
        new Client { Name = "Walton", Logo = "WALTON", Order = 6 },
        new Client { Name = "PRAN-RFL", Logo = "PRAN", Order = 7 },
        new Client { Name = "Bashundhara", Logo = "BASHUNDHARA", Order = 8 },
        new Client { Name = "Jamuna Future Park", Logo = "JFP", Order = 9 },
        new Client { Name = "United Hospital", Logo = "UNITED HOSPITAL", Order = 10 },
        new Client { Name = "BUET", Logo = "BUET", Order = 11 },
        new Client { Name = "BRAC", Logo = "BRAC", Order = 12 },
    };

    // ---------- TESTIMONIALS (6) ----------
    public static readonly List<Testimonial> Testimonials = new()
    {
        new Testimonial { Name = "Md. Tanvir Mahmud", Company = "Beximco Industrial Park", Role = "Head of Operations", Rating = 5, Order = 1, Published = true,
            Message = "SecureShield has been guarding our industrial park for 4 years. Their guards are disciplined, well-trained and their control room responds within minutes." },
        new Testimonial { Name = "Nusrat Jahan", Company = "BRAC Bank Ltd.", Role = "Facilities Manager", Rating = 5, Order = 2, Published = true,
            Message = "From armed guards at our main branch to reception security at HQ, SecureShield delivers consistently. Their paperwork, background checks and SOPs are of international standard." },
        new Testimonial { Name = "Andreas Müller", Company = "InterContinental Dhaka", Role = "Director of Security", Rating = 5, Order = 3, Published = true,
            Message = "As a 5-star hotel we cannot compromise on guest experience. SecureShield's guards are professional, English-speaking and discreet." },
        new Testimonial { Name = "Dr. Aminul Haque", Company = "United Hospital", Role = "Director", Rating = 5, Order = 4, Published = true,
            Message = "Hospital security is sensitive — crowd control at OPD, ICU access and patient privacy. SecureShield trained their team specifically for healthcare." },
        new Testimonial { Name = "K. M. Anwarul Islam", Company = "Bashundhara Group", Role = "General Manager — Security", Rating = 5, Order = 5, Published = true,
            Message = "We deployed SecureShield across our residential complexes and shopping malls. Their nationwide coverage and 24/7 control room give us peace of mind." },
        new Testimonial { Name = "Sarah Williams", Company = "Save the Children Bangladesh", Role = "Country Office Manager", Rating = 5, Order = 6, Published = true,
            Message = "Our NGO field offices needed trustworthy security. SecureShield's background-verified guards and rapid response team fit our needs perfectly." },
    };

    // ---------- GALLERY ITEMS (12) ----------
    public static readonly List<GalleryItem> GalleryItems = new()
    {
        new GalleryItem { Title = "On Duty — Factory Gate", Category = "duty", Order = 1, ImageUrl = "/images/gallery-1.jpg", Caption = "Industrial facility entry control" },
        new GalleryItem { Title = "Morning Drill", Category = "training", Order = 2, ImageUrl = "/images/gallery-2.jpg", Caption = "Daily physical training session" },
        new GalleryItem { Title = "Corporate Lobby Post", Category = "office", Order = 3, ImageUrl = "/images/gallery-3.jpg", Caption = "Reception security at corporate HQ" },
        new GalleryItem { Title = "Event Crowd Management", Category = "events", Order = 4, ImageUrl = "/images/gallery-4.jpg", Caption = "Conference event stewarding" },
        new GalleryItem { Title = "Fire Safety Drill", Category = "training", Order = 5, ImageUrl = "/images/gallery-5.jpg", Caption = "Quarterly fire-fighting exercise" },
        new GalleryItem { Title = "Warehouse Patrol", Category = "duty", Order = 6, ImageUrl = "/images/gallery-6.jpg", Caption = "Distribution center night patrol" },
        new GalleryItem { Title = "Control Room", Category = "office", Order = 7, ImageUrl = "/images/gallery-7.jpg", Caption = "24/7 monitoring operations" },
        new GalleryItem { Title = "Mall Security Team", Category = "events", Order = 8, ImageUrl = "/images/gallery-8.jpg", Caption = "Shopping mall guarding team" },
        new GalleryItem { Title = "VIP Escort", Category = "duty", Order = 9, ImageUrl = "/images/gallery-9.jpg", Caption = "Executive close protection" },
        new GalleryItem { Title = "First Aid Training", Category = "training", Order = 10, ImageUrl = "/images/gallery-10.jpg", Caption = "Certified first-aid module" },
        new GalleryItem { Title = "Hospital Reception", Category = "office", Order = 11, ImageUrl = "/images/gallery-11.jpg", Caption = "Hospital access management" },
        new GalleryItem { Title = "Construction Site Patrol", Category = "duty", Order = 12, ImageUrl = "/images/gallery-12.jpg", Caption = "Perimeter patrol at construction site" },
    };

    // ---------- GUARD PROFILES (10) ----------
    public static readonly List<GuardProfile> GuardProfiles = new()
    {
        new GuardProfile { Name = "Rakib Hasan", Category = "male", Role = "Senior Security Guard", Order = 1, ImageUrl = "/images/guard-1.jpg" },
        new GuardProfile { Name = "Karim Uddin", Category = "male", Role = "Corporate Security Officer", Order = 2, ImageUrl = "/images/guard-2.jpg" },
        new GuardProfile { Name = "Nadia Akter", Category = "female", Role = "Reception Security Officer", Order = 3, ImageUrl = "/images/guard-3.jpg" },
        new GuardProfile { Name = "Fatima Rahman", Category = "female", Role = "Hospital Security Guard", Order = 4, ImageUrl = "/images/guard-4.jpg" },
        new GuardProfile { Name = "Imran Khan", Category = "armed", Role = "Armed Guard — Bank", Order = 5, ImageUrl = "/images/guard-5.jpg" },
        new GuardProfile { Name = "Jahidul Islam", Category = "armed", Role = "Armed Cash-in-Transit Guard", Order = 6, ImageUrl = "/images/guard-6.jpg" },
        new GuardProfile { Name = "Tanvir Ahmed", Category = "vip", Role = "VIP Protection Officer", Order = 7, ImageUrl = "/images/guard-7.jpg" },
        new GuardProfile { Name = "Sabbir Hossain", Category = "vip", Role = "Close Protection Officer", Order = 8, ImageUrl = "/images/guard-8.jpg" },
        new GuardProfile { Name = "Mizanur Rahman", Category = "supervisor", Role = "Field Supervisor", Order = 9, ImageUrl = "/images/guard-9.jpg" },
        new GuardProfile { Name = "Anwar Hossain", Category = "supervisor", Role = "Shift In-charge", Order = 10, ImageUrl = "/images/guard-10.jpg" },
    };

    // ---------- SITE SETTINGS (8) ----------
    public static readonly List<SiteSetting> SiteSettings = new()
    {
        new SiteSetting { Key = "site.title", Value = "USS Security Services Ltd." },
        new SiteSetting { Key = "site.tagline", Value = "Your Security, Our Commitment" },
        new SiteSetting { Key = "site.email", Value = "info@uss-security.com.bd" },
        new SiteSetting { Key = "site.phone", Value = "+8801700000000" },
        new SiteSetting { Key = "site.address", Value = "Level 6, Plot 37, Gulshan Avenue, Gulshan-1, Dhaka 1212, Bangladesh" },
        new SiteSetting { Key = "seo.title", Value = "USS Security Services Ltd. | Your Security, Our Commitment — Bangladesh" },
        new SiteSetting { Key = "seo.description", Value = "Licensed security personnel supplier providing highly trained, military-grade guards across Bangladesh since 2010." },
        new SiteSetting { Key = "seo.keywords", Value = "USS Security, security services Bangladesh, security guards Dhaka, corporate security, VIP protection, CCTV monitoring, fire safety" },
    };

    // ---------- ADMIN CREDENTIALS (hardcoded — change in production) ----------
    public const string AdminEmail = "admin@uss-security.com.bd";
    public const string AdminPassword = "Admin@123";
    public const string AdminName = "Site Administrator";
    public const string AdminRole = "admin";
}
