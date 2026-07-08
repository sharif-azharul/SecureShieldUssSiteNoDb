using System.Text.Json;

namespace SecureShield.Application.Services;

/// <summary>
/// Bilingual (English / বাংলা) localization service for USS Security.
/// Scoped — each browser session gets its own language preference.
/// </summary>
public interface ILocalizationService
{
    /// <summary>Current language code: "en" or "bn".</summary>
    string CurrentLang { get; }

    /// <summary>True when current language is Bangla.</summary>
    bool IsBn { get; }

    /// <summary>Toggle between en ↔ bn. Returns new lang.</summary>
    string Toggle();

    /// <summary>Set language explicitly.</summary>
    void SetLang(string lang);

    /// <summary>Get localized string by key.</summary>
    string Get(string key);

    /// <summary>Indexer shortcut for Get(key).</summary>
    string this[string key] { get; }

    /// <summary>Pick one of two values based on current language.</summary>
    string T(string en, string bn);
}

public class LocalizationService : ILocalizationService
{
    public string CurrentLang { get; private set; } = "en";
    public bool IsBn => CurrentLang == "bn";

    public string Toggle()
    {
        CurrentLang = CurrentLang == "en" ? "bn" : "en";
        return CurrentLang;
    }

    public void SetLang(string lang)
    {
        if (lang == "en" || lang == "bn") CurrentLang = lang;
    }

    public string Get(string key) =>
        _dict.TryGetValue(key, out var entry) && entry.TryGetValue(CurrentLang, out var val)
            ? val : key;

    public string this[string key] => Get(key);

    public string T(string en, string bn) => IsBn ? bn : en;

    private static readonly Dictionary<string, Dictionary<string, string>> _dict = new()
    {
        // ============ BRAND ============
        ["brand.name"] = new() { ["en"] = "USS Security", ["bn"] = "ইউএসএস সিকিউরিটি" },
        ["brand.full"] = new() { ["en"] = "USS Security Services Ltd.", ["bn"] = "ইউএসএস সিকিউরিটি সার্ভিসেস লি." },
        ["brand.tagline"] = new() { ["en"] = "Your Security, Our Commitment", ["bn"] = "আপনার নিরাপত্তা, আমাদের অঙ্গীকার" },

        // ============ HEADER ============
        ["header.phone"] = new() { ["en"] = "+880 1700-000000", ["bn"] = "+880 1700-000000" },
        ["header.email"] = new() { ["en"] = "info@uss-security.com.bd", ["bn"] = "info@uss-security.com.bd" },
        ["header.licensed"] = new() { ["en"] = "Licensed Security Service Provider — Govt. of Bangladesh", ["bn"] = "বাংলাদেশ সরকার কর্তৃক লাইসেন্সপ্রাপ্ত নিরাপত্তা সেবা প্রদানকারী" },

        ["nav.home"] = new() { ["en"] = "Home", ["bn"] = "হোম" },
        ["nav.about"] = new() { ["en"] = "About", ["bn"] = "পরিচিতি" },
        ["nav.services"] = new() { ["en"] = "Services", ["bn"] = "সেবাসমূহ" },
        ["nav.process"] = new() { ["en"] = "Process", ["bn"] = "প্রক্রিয়া" },
        ["nav.industries"] = new() { ["en"] = "Industries", ["bn"] = "শিল্পখাত" },
        ["nav.why"] = new() { ["en"] = "Why Us", ["bn"] = "কেন আমরা" },
        ["nav.guards"] = new() { ["en"] = "Guards", ["bn"] = "গার্ড" },
        ["nav.training"] = new() { ["en"] = "Training", ["bn"] = "প্রশিক্ষণ" },
        ["nav.gallery"] = new() { ["en"] = "Gallery", ["bn"] = "গ্যালারী" },
        ["nav.career"] = new() { ["en"] = "Career", ["bn"] = "ক্যারিয়ার" },
        ["nav.contact"] = new() { ["en"] = "Contact", ["bn"] = "যোগাযোগ" },
        ["nav.admin"] = new() { ["en"] = "Admin", ["bn"] = "অ্যাডমিন" },
        ["nav.quote"] = new() { ["en"] = "Get Free Quotation", ["bn"] = "ফ্রি কোটেশন নিন" },
        ["lang.toggle"] = new() { ["en"] = "বাংলা", ["bn"] = "English" },

        // ============ HERO ============
        ["hero.eyebrow"] = new() { ["en"] = "LICENSED · MILITARY-GRADE · 24/7 NATIONWIDE", ["bn"] = "লাইসেন্সপ্রাপ্ত · সামরিক-মানের · ২৪/৭ সারাদেশ" },
        ["hero.title1"] = new() { ["en"] = "YOUR SECURITY", ["bn"] = "আপনার নিরাপত্তা" },
        ["hero.title2"] = new() { ["en"] = "OUR COMMITMENT", ["bn"] = "আমাদের অঙ্গীকার" },
        ["hero.subhead"] = new()
        {
            ["en"] = "Professional Security Personnel Across Bangladesh — highly trained, background-verified guards for industries, banks, hospitals, factories, malls, residential complexes, offices and events.",
            ["bn"] = "বাংলাদেশজুড়ে পেশাদার নিরাপত্তা কর্মী — শিল্প, ব্যাংক, হাসপাতাল, কারখানা, মল, আবাসিক কমপ্লেক্স, অফিস ও অনুষ্ঠানের জন্য উচ্চ প্রশিক্ষিত, ব্যাকগ্রাউন্ড-যাচাইকৃত গার্ড।"
        },
        ["hero.ctaQuote"] = new() { ["en"] = "📋 Get Free Quotation", ["bn"] = "📋 ফ্রি কোটেশন নিন" },
        ["hero.ctaContact"] = new() { ["en"] = "✉ Contact Us", ["bn"] = "✉ যোগাযোগ করুন" },
        ["hero.ctaCall"] = new() { ["en"] = "📞 Call Now", ["bn"] = "📞 এখনই কল করুন" },
        ["hero.trust1"] = new() { ["en"] = "200+ Corporate Clients", ["bn"] = "২০০+ কর্পোরেট ক্লায়েন্ট" },
        ["hero.trust2"] = new() { ["en"] = "ISO-aligned Operations", ["bn"] = "আইএসও-সঙ্গত পরিচালনা" },
        ["hero.trust3"] = new() { ["en"] = "500+ Trained Guards", ["bn"] = "৫০০+ প্রশিক্ষিত গার্ড" },
        ["hero.trust4"] = new() { ["en"] = "All 64 Districts Covered", ["bn"] = "সব ৬৪ জেলায় কভারেজ" },

        // ============ STATS ============
        ["stats.guards"] = new() { ["en"] = "Professional Guards", ["bn"] = "পেশাদার গার্ড" },
        ["stats.clients"] = new() { ["en"] = "Corporate Clients", ["bn"] = "কর্পোরেট ক্লায়েন্ট" },
        ["stats.experience"] = new() { ["en"] = "Years Experience", ["bn"] = "বছরের অভিজ্ঞতা" },
        ["stats.satisfaction"] = new() { ["en"] = "Client Satisfaction", ["bn"] = "ক্লায়েন্ট সন্তুষ্টি" },

        // ============ ABOUT ============
        ["about.label"] = new() { ["en"] = "ABOUT USS SECURITY", ["bn"] = "ইউএসএস সিকিউরিটি সম্পর্কে" },
        ["about.title"] = new() { ["en"] = "Bangladesh's Trusted Name in Professional Security", ["bn"] = "পেশাদার নিরাপত্তায় বাংলাদেশের বিশ্বস্ত নাম" },
        ["about.p1"] = new()
        {
            ["en"] = "USS Security Services Ltd. is a licensed, ISO-aligned security personnel supplier headquartered in Dhaka, Bangladesh. Since 2010 we have protected the country's leading industries, banks, hospitals, retail complexes, factories and residential communities with rigorously vetted, military-trained guards.",
            ["bn"] = "ইউএসএস সিকিউরিটি সার্ভিসেস লি. ঢাকা, বাংলাদেশে অবস্থিত একটি লাইসেন্সপ্রাপ্ত, আইএসও-সঙ্গত নিরাপত্তা কর্মী সরবরাহকারী প্রতিষ্ঠান। ২০১০ সাল থেকে আমরা দেশের শীর্ষস্থানীয় শিল্প, ব্যাংক, হাসপাতাল, খুচরা কমপ্লেক্স, কারখানা ও আবাসিক সম্প্রদায়কে কঠোরভাবে যাচাইকৃত, সামরিক-প্রশিক্ষিত গার্ড দিয়ে সুরক্ষিত করছি।"
        },
        ["about.p2"] = new()
        {
            ["en"] = "Our 500+ guards undergo structured physical, fire-safety, first-aid and customer-service training before deployment. We operate under a documented chain of command, with field supervisors, a 24/7 control room, and a rapid emergency response team covering all 64 districts of Bangladesh.",
            ["bn"] = "আমাদের ৫০০+ গার্ড মোতায়েনের আগে কাঠামোগত শারীরিক, অগ্নি নিরাপত্তা, প্রাথমিক চিকিৎসা ও গ্রাহক সেবা প্রশিক্ষণ গ্রহণ করে। আমরা একটি নথিভুক্ত কমান্ড চেইনের অধীনে পরিচালিত হই, ফিল্ড সুপারভাইজার, ২৪/৭ কন্ট্রোল রুম এবং বাংলাদেশের সকল ৬৪ জেলা কভার করে একটি দ্রুত জরুরি প্রতিক্রিয়া দল নিয়ে।"
        },
        ["about.bullet1"] = new() { ["en"] = "Licensed by Government of Bangladesh", ["bn"] = "বাংলাদেশ সরকার কর্তৃক লাইসেন্সপ্রাপ্ত" },
        ["about.bullet2"] = new() { ["en"] = "ISO-aligned operational procedures", ["bn"] = "আইএসও-সঙ্গত পরিচালন পদ্ধতি" },
        ["about.bullet3"] = new() { ["en"] = "100% background-verified personnel", ["bn"] = "১০০% ব্যাকগ্রাউন্ড-যাচাইকৃত কর্মী" },
        ["about.bullet4"] = new() { ["en"] = "Nationwide coverage — all 64 districts", ["bn"] = "সারাদেশীয় কভারেজ — সব ৬৪ জেলা" },
        ["about.mission"] = new() { ["en"] = "Our Mission", ["bn"] = "আমাদের লক্ষ্য" },
        ["about.missionText"] = new()
        {
            ["en"] = "To safeguard our clients' people, property and operations through disciplined, well-trained and accountable security personnel — delivering peace of mind 24/7.",
            ["bn"] = "শৃঙ্খলাবদ্ধ, সুপ্রশিক্ষিত ও জবাবদিহিমূলক নিরাপত্তা কর্মীর মাধ্যমে আমাদের ক্লায়েন্টদের মানুষ, সম্পদ ও পরিচালনাকে সুরক্ষিত রাখা — ২৪/৭ নিশ্চিন্ত মন প্রদান করা।"
        },
        ["about.vision"] = new() { ["en"] = "Our Vision", ["bn"] = "আমাদের দৃষ্টিভঙ্গি" },
        ["about.visionText"] = new()
        {
            ["en"] = "To be the most trusted and professionally respected security services company in Bangladesh, setting the national benchmark for guard training and operational integrity.",
            ["bn"] = "বাংলাদেশের সবচেয়ে বিশ্বস্ত ও পেশাদারভাবে সম্মানিত নিরাপত্তা সেবা কোম্পানি হওয়া, গার্ড প্রশিক্ষণ ও পরিচালন সততায় জাতীয় মানদণ্ড স্থাপন করা।"
        },
        ["about.badge"] = new() { ["en"] = "Licensed & Insured", ["bn"] = "লাইসেন্সপ্রাপ্ত ও বীমাকৃত" },
        ["about.yearsLabel"] = new() { ["en"] = "Years of Excellence", ["bn"] = "উৎকর্ষের বছর" },

        // ============ SERVICES ============
        ["services.label"] = new() { ["en"] = "WHAT WE PROVIDE", ["bn"] = "আমরা যা প্রদান করি" },
        ["services.title"] = new() { ["en"] = "Our Security Services", ["bn"] = "আমাদের নিরাপত্তা সেবাসমূহ" },
        ["services.sub"] = new()
        {
            ["en"] = "Comprehensive security personnel solutions tailored to every sector of the Bangladeshi economy — 16 specialized services delivered by military-trained professionals.",
            ["bn"] = "বাংলাদেশের অর্থনীতির প্রতিটি খাতের জন্য উপযোগী বিস্তৃত নিরাপত্তা কর্মী সমাধান — সামরিক-প্রশিক্ষিত পেশাদারদের দ্বারা পরিচালিত ১৬টি বিশেষায়িত সেবা।"
        },

        // ============ SECURITY PROCESS ============
        ["process.label"] = new() { ["en"] = "HOW WE WORK", ["bn"] = "আমরা যেভাবে কাজ করি" },
        ["process.title"] = new() { ["en"] = "Our Security Process", ["bn"] = "আমাদের নিরাপত্তা প্রক্রিয়া" },
        ["process.sub"] = new()
        {
            ["en"] = "A structured 5-step methodology ensuring every client receives tailored, military-grade security — from initial consultation to 24/7 monitoring.",
            ["bn"] = "একটি কাঠামোগত ৫-ধাপের পদ্ধতি যা নিশ্চিত করে প্রতিটি ক্লায়েন্ট প্রাথমিক পরামর্শ থেকে ২৪/৭ মনিটরিং পর্যন্ত উপযোগী, সামরিক-মানের নিরাপত্তা পান।"
        },
        ["process.step1.title"] = new() { ["en"] = "Requirement Analysis", ["bn"] = "প্রয়োজনীয়তা বিশ্লেষণ" },
        ["process.step1.desc"] = new() { ["en"] = "We sit with your team to understand threats, asset profile, operational hours, and compliance needs.", ["bn"] = "আমরা আপনার টিমের সাথে বসে হুমকি, সম্পদের প্রোফাইল, কার্য সময় ও সম্মতি চাহিদা বুঝতে পারি।" },
        ["process.step2.title"] = new() { ["en"] = "Risk Assessment", ["bn"] = "ঝুঁকি মূল্যায়ন" },
        ["process.step2.desc"] = new() { ["en"] = "Our security consultants audit your premises, identify vulnerabilities, and score risk across all entry points.", ["bn"] = "আমাদের নিরাপত্তা পরামর্শকরা আপনার প্রাঙ্গণ পরিদর্শন করেন, দুর্বলতা শনাক্ত করেন এবং সব এন্ট্রি পয়েন্টে ঝুঁকি মূল্যায়ন করেন।" },
        ["process.step3.title"] = new() { ["en"] = "Personnel Selection", ["bn"] = "কর্মী নির্বাচন" },
        ["process.step3.desc"] = new() { ["en"] = "We match background-verified, trained guards to your industry — bank, hospital, factory, residential or event.", ["bn"] = "আমরা ব্যাকগ্রাউন্ড-যাচাইকৃত, প্রশিক্ষিত গার্ড আপনার শিল্পের সাথে মিলিয়ে দিই — ব্যাংক, হাসপাতাল, কারখানা, আবাসিক বা অনুষ্ঠান।" },
        ["process.step4.title"] = new() { ["en"] = "Deployment", ["bn"] = "মোতায়েন" },
        ["process.step4.desc"] = new() { ["en"] = "Uniformed guards deploy with documented SOPs, reporting cadence, and a dedicated field supervisor.", ["bn"] = "ইউনিফর্ম পরিহিত গার্ডরা নথিভুক্ত SOP, রিপোর্টিং ক্যাডেন্স এবং একজন নিবেদিত ফিল্ড সুপারভাইজার নিয়ে মোতায়েন হন।" },
        ["process.step5.title"] = new() { ["en"] = "24/7 Monitoring", ["bn"] = "২৪/৭ মনিটরিং" },
        ["process.step5.desc"] = new() { ["en"] = "Our control room monitors every post round-the-clock with rapid-response teams across all 64 districts.", ["bn"] = "আমাদের কন্ট্রোল রুম প্রতিটি পোস্ট চব্বিশ ঘন্টা মনিটর করে সব ৬৪ জেলায় দ্রুত-প্রতিক্রিয়া দল নিয়ে।" },

        // ============ INDUSTRIES ============
        ["industries.label"] = new() { ["en"] = "SECTOR COVERAGE", ["bn"] = "খাত কভারেজ" },
        ["industries.title"] = new() { ["en"] = "Industries We Serve", ["bn"] = "আমরা যেসব শিল্পখাতে সেবা দিই" },
        ["industries.sub"] = new()
        {
            ["en"] = "Trusted by leading organizations across every major industry in Bangladesh — from garments and banking to healthcare and government.",
            ["bn"] = "বাংলাদেশের প্রতিটি প্রধান শিল্পখাতের শীর্ষস্থানীয় প্রতিষ্ঠানের আস্থায় স্থান পেয়েছি — তৈরি পোশাক ও ব্যাংকিং থেকে স্বাস্থ্যসেবা ও সরকার পর্যন্ত।"
        },

        // ============ WHY CHOOSE US ============
        ["why.label"] = new() { ["en"] = "OUR ADVANTAGES", ["bn"] = "আমাদের সুবিধা" },
        ["why.title"] = new() { ["en"] = "Why Choose USS Security", ["bn"] = "ইউএসএস সিকিউরিটি কেন বেছে নিবেন" },
        ["why.sub"] = new()
        {
            ["en"] = "Eight reasons organizations across Bangladesh trust USS Security with their most valuable assets — and their people.",
            ["bn"] = "বাংলাদেশজুড়ে প্রতিষ্ঠানগুলো তাদের সবচেয়ে মূল্যবান সম্পদ — এবং মানুষ — ইউএসএস সিকিউরিটির কাছে নির্ভর করার আটটি কারণ।"
        },
        ["why.item1.title"] = new() { ["en"] = "24/7 Support", ["bn"] = "২৪/৭ সহায়তা" },
        ["why.item1.desc"] = new() { ["en"] = "Round-the-clock control room and field supervisors reachable any hour.", ["bn"] = "চব্বিশ ঘন্টা কন্ট্রোল রুম ও ফিল্ড সুপারভাইজার যেকোনো সময় সহজে অ্যাক্সেসযোগ্য।" },
        ["why.item2.title"] = new() { ["en"] = "Licensed Company", ["bn"] = "লাইসেন্সপ্রাপ্ত কোম্পানি" },
        ["why.item2.desc"] = new() { ["en"] = "Registered and licensed security service provider under Bangladesh law.", ["bn"] = "বাংলাদেশ আইনে নিবন্ধিত ও লাইসেন্সপ্রাপ্ত নিরাপত্তা সেবা প্রদানকারী।" },
        ["why.item3.title"] = new() { ["en"] = "Uniformed Guards", ["bn"] = "ইউনিফর্ম গার্ড" },
        ["why.item3.desc"] = new() { ["en"] = "Disciplined, well-groomed guards in standardized company uniform.", ["bn"] = "মানসম্মত কোম্পানি ইউনিফর্মে সুশৃঙ্খল, পরিপাটি গার্ড।" },
        ["why.item4.title"] = new() { ["en"] = "Background Verified", ["bn"] = "ব্যাকগ্রাউন্ড যাচাইকৃত" },
        ["why.item4.desc"] = new() { ["en"] = "Police verification, address check and reference validation for every guard.", ["bn"] = "প্রতিটি গার্ডের জন্য পুলিশ যাচাই, ঠিকানা যাচাই ও রেফারেন্স বৈধতা।" },
        ["why.item5.title"] = new() { ["en"] = "Well Trained Staff", ["bn"] = "সুপ্রশিক্ষিত কর্মী" },
        ["why.item5.desc"] = new() { ["en"] = "Structured 7-module pre-deployment training for every guard.", ["bn"] = "প্রতিটি গার্ডের জন্য কাঠামোগত ৭-মডিউল প্রি-ডিপ্লয়মেন্ট প্রশিক্ষণ।" },
        ["why.item6.title"] = new() { ["en"] = "Emergency Response", ["bn"] = "জরুরি প্রতিক্রিয়া" },
        ["why.item6.desc"] = new() { ["en"] = "Dedicated rapid-response team deployable within 60 minutes in major cities.", ["bn"] = "প্রধান শহরগুলোতে ৬০ মিনিটের মধ্যে মোতায়েনযোগ্য নিবেদিত দ্রুত-প্রতিক্রিয়া দল।" },
        ["why.item7.title"] = new() { ["en"] = "Affordable Pricing", ["bn"] = "সাশ্রয়ী মূল্য" },
        ["why.item7.desc"] = new() { ["en"] = "Competitive, transparent monthly rates with no hidden charges.", ["bn"] = "কোনো লুকানো চার্জ ছাড়া প্রতিযোগিতামূলক, স্বচ্ছ মাসিক হার।" },
        ["why.item8.title"] = new() { ["en"] = "Nationwide Coverage", ["bn"] = "সারাদেশীয় কভারেজ" },
        ["why.item8.desc"] = new() { ["en"] = "Operational presence in all 64 districts of Bangladesh.", ["bn"] = "বাংলাদেশের সকল ৬৪ জেলায় পরিচালন উপস্থিতি।" },

        // ============ TRAINING ============
        ["training.label"] = new() { ["en"] = "HOW WE TRAIN", ["bn"] = "আমরা যেভাবে প্রশিক্ষণ দিই" },
        ["training.title"] = new() { ["en"] = "Guard Training Program", ["bn"] = "গার্ড প্রশিক্ষণ প্রোগ্রাম" },
        ["training.sub"] = new()
        {
            ["en"] = "Every guard completes a structured 7-module training program before deployment — military-style discipline combined with modern security protocols.",
            ["bn"] = "প্রতিটি গার্ড মোতায়েনের আগে একটি কাঠামোগত ৭-মডিউল প্রশিক্ষণ প্রোগ্রাম সম্পন্ন করে — সামরিক-ধাঁচের শৃঙ্খলা ও আধুনিক নিরাপত্তা প্রোটোকলের সমন্বয়।"
        },
        ["training.module1.title"] = new() { ["en"] = "Physical Training", ["bn"] = "শারীরিক প্রশিক্ষণ" },
        ["training.module1.desc"] = new() { ["en"] = "Stamina, drill, posture and self-defense fundamentals.", ["bn"] = "স্ট্যামিনা, ড্রিল, পোস্টার ও সেলফ-ডিফেন্স মৌলিক বিষয়।" },
        ["training.module2.title"] = new() { ["en"] = "Fire Fighting", ["bn"] = "অগ্নি নিরাবরণ" },
        ["training.module2.desc"] = new() { ["en"] = "Use of extinguishers, evacuation drills and fire-zone discipline.", ["bn"] = "এক্সটিংগুইশার ব্যবহার, একুয়েশন ড্রিল ও ফায়ার-জোন শৃঙ্খলা।" },
        ["training.module3.title"] = new() { ["en"] = "Emergency Response", ["bn"] = "জরুরি প্রতিক্রিয়া" },
        ["training.module3.desc"] = new() { ["en"] = "Drills for medical, fire, intrusion and natural-disaster scenarios.", ["bn"] = "মেডিকেল, অগ্নি, অনুপ্রবেশ ও প্রাকৃতিক-দুর্যোগ পরিস্থিতির জন্য ড্রিল।" },
        ["training.module4.title"] = new() { ["en"] = "Customer Service", ["bn"] = "গ্রাহক সেবা" },
        ["training.module4.desc"] = new() { ["en"] = "Guest handling, etiquette and professional communication.", ["bn"] = "অতিথি পরিচালনা, শিষ্টাচার ও পেশাদার যোগাযোগ।" },
        ["training.module5.title"] = new() { ["en"] = "First Aid", ["bn"] = "প্রাথমিক চিকিৎসা" },
        ["training.module5.desc"] = new() { ["en"] = "Certified first-aid and CPR training for on-site emergencies.", ["bn"] = "অন-সাইট জরুরি অবস্থার জন্য সার্টিফায়েড ফার্স্ট-এইড ও সিপিআর প্রশিক্ষণ।" },
        ["training.module6.title"] = new() { ["en"] = "Crowd Control", ["bn"] = "ভিড় নিয়ন্ত্রণ" },
        ["training.module6.desc"] = new() { ["en"] = "Queue management, barrier discipline and de-escalation tactics.", ["bn"] = "কিউ ব্যবস্থাপনা, ব্যারিয়ার শৃঙ্খলা ও ডি-এসকেলেশন কৌশল।" },
        ["training.module7.title"] = new() { ["en"] = "Risk Assessment", ["bn"] = "ঝুঁকি মূল্যায়ন" },
        ["training.module7.desc"] = new() { ["en"] = "Identifying threats, vulnerability scoring and incident reporting.", ["bn"] = "হুমকি শনাক্তকরণ, দুর্বলতা স্কোরিং ও ঘটনা রিপোর্টিং।" },
        ["training.stat1.value"] = new() { ["en"] = "120+ hours", ["bn"] = "১২০+ ঘন্টা" },
        ["training.stat1.label"] = new() { ["en"] = "Pre-deployment training", ["bn"] = "প্রি-ডিপ্লয়মেন্ট প্রশিক্ষণ" },
        ["training.stat2.value"] = new() { ["en"] = "Certified", ["bn"] = "সার্টিফায়েড" },
        ["training.stat2.label"] = new() { ["en"] = "Fire safety & first-aid", ["bn"] = "অগ্নি নিরাপত্তা ও প্রাথমিক চিকিৎসা" },
        ["training.stat3.value"] = new() { ["en"] = "Quarterly", ["bn"] = "ত্রৈমাসিক" },
        ["training.stat3.label"] = new() { ["en"] = "Refresher modules", ["bn"] = "রিফ্রেশার মডিউল" },

        // ============ GUARDS ============
        ["guards.label"] = new() { ["en"] = "MEET THE TEAM", ["bn"] = "টিমের সাথে পরিচিত হন" },
        ["guards.title"] = new() { ["en"] = "Our Professional Guards", ["bn"] = "আমাদের পেশাদার গার্ড" },
        ["guards.sub"] = new()
        {
            ["en"] = "Military-trained, background-verified personnel across multiple categories — ready for deployment nationwide.",
            ["bn"] = "একাধিক বিভাগে সামরিক-প্রশিক্ষিত, ব্যাকগ্রাউন্ড-যাচাইকৃত কর্মী — সারাদেশে মোতায়েনের জন্য প্রস্তুত।"
        },
        ["guards.filterAll"] = new() { ["en"] = "All", ["bn"] = "সব" },
        ["guards.filterMale"] = new() { ["en"] = "Male Guards", ["bn"] = "পুরুষ গার্ড" },
        ["guards.filterFemale"] = new() { ["en"] = "Female Guards", ["bn"] = "নারী গার্ড" },
        ["guards.filterArmed"] = new() { ["en"] = "Armed Guards", ["bn"] = "সশস্ত্র গার্ড" },
        ["guards.filterVip"] = new() { ["en"] = "VIP Guards", ["bn"] = "ভিআইপি গার্ড" },
        ["guards.filterSupervisor"] = new() { ["en"] = "Supervisors", ["bn"] = "সুপারভাইজার" },

        // ============ CLIENTS ============
        ["clients.label"] = new() { ["en"] = "TRUSTED BY", ["bn"] = "যাদের আস্থা" },
        ["clients.title"] = new() { ["en"] = "Our Valued Clients", ["bn"] = "আমাদের মূল্যবান ক্লায়েন্ট" },
        ["clients.sub"] = new()
        {
            ["en"] = "Leading brands and institutions across Bangladesh rely on USS Security for their most critical security needs.",
            ["bn"] = "বাংলাদেশজুড়ে শীর্ষস্থানীয় ব্র্যান্ড ও প্রতিষ্ঠান তাদের সবচেয়ে গুরুত্বপূর্ণ নিরাপত্তা চাহিদার জন্য ইউএসএস সিকিউরিটির উপর নির্ভর করে।"
        },
        ["clients.testimonialLabel"] = new() { ["en"] = "CLIENT TESTIMONIALS", ["bn"] = "ক্লায়েন্ট প্রশংসা" },
        ["clients.testimonialTitle"] = new() { ["en"] = "What Our Clients Say", ["bn"] = "আমাদের ক্লায়েন্টরা যা বলেন" },

        // ============ GALLERY ============
        ["gallery.label"] = new() { ["en"] = "IN ACTION", ["bn"] = "কাজের মধ্যে" },
        ["gallery.title"] = new() { ["en"] = "Photo Gallery", ["bn"] = "ফটো গ্যালারী" },
        ["gallery.sub"] = new()
        {
            ["en"] = "A glimpse of our guards, training sessions, events and on-duty operations across Bangladesh.",
            ["bn"] = "বাংলাদেশজুড়ে আমাদের গার্ড, প্রশিক্ষণ সেশন, অনুষ্ঠান ও ডিউটি পরিচালনার এক ঝলক।"
        },
        ["gallery.filterAll"] = new() { ["en"] = "All", ["bn"] = "সব" },
        ["gallery.filterDuty"] = new() { ["en"] = "Guard Duty", ["bn"] = "ডিউটি" },
        ["gallery.filterTraining"] = new() { ["en"] = "Training", ["bn"] = "প্রশিক্ষণ" },
        ["gallery.filterEvents"] = new() { ["en"] = "Events", ["bn"] = "অনুষ্ঠান" },
        ["gallery.filterOffice"] = new() { ["en"] = "Office", ["bn"] = "অফিস" },
        ["gallery.catDuty"] = new() { ["en"] = "GUARD DUTY", ["bn"] = "ডিউটি" },
        ["gallery.catTraining"] = new() { ["en"] = "TRAINING", ["bn"] = "প্রশিক্ষণ" },
        ["gallery.catEvents"] = new() { ["en"] = "EVENTS", ["bn"] = "অনুষ্ঠান" },
        ["gallery.catOffice"] = new() { ["en"] = "OFFICE", ["bn"] = "অফিস" },

        // ============ CAREER ============
        ["career.label"] = new() { ["en"] = "JOIN OUR TEAM", ["bn"] = "আমাদের টিমে যোগ দিন" },
        ["career.title"] = new() { ["en"] = "Build Your Career With USS Security", ["bn"] = "ইউএসএস সিকিউরিটিতে ক্যারিয়ার গড়ুন" },
        ["career.sub"] = new()
        {
            ["en"] = "We're hiring trained security professionals across Bangladesh. Submit your application below — our HR team responds within 7 working days.",
            ["bn"] = "আমরা সারাবাংলাদেশে প্রশিক্ষিত নিরাপত্তা পেশাদার নিয়োগ করছি। নিচে আপনার আবেদন জমা দিন — আমাদের এইচআর টিম ৭ কর্মদিবসের মধ্যে সাড়া দেয়।"
        },
        ["career.whyTitle"] = new() { ["en"] = "Why work with us?", ["bn"] = "কেন আমাদের সাথে কাজ করবেন?" },
        ["career.benefit1"] = new() { ["en"] = "Competitive monthly salary with overtime", ["bn"] = "ওভারটাইম সহ প্রতিযোগিতামূলক মাসিক বেতন" },
        ["career.benefit2"] = new() { ["en"] = "Free pre-deployment & refresher training", ["bn"] = "ফ্রি প্রি-ডিপ্লয়মেন্ট ও রিফ্রেশার প্রশিক্ষণ" },
        ["career.benefit3"] = new() { ["en"] = "Permanent positions with PF & gratuity", ["bn"] = "পিএফ ও গ্র্যাচুইটি সহ স্থায়ী পদ" },
        ["career.benefit4"] = new() { ["en"] = "Deployments across all 64 districts", ["bn"] = "সব ৬৪ জেলায় মোতায়েন" },
        ["career.benefit5"] = new() { ["en"] = "Background verification & uniform provided", ["bn"] = "ব্যাকগ্রাউন্ড যাচাই ও ইউনিফর্ম সরবরাহ" },
        ["career.hrHelpline"] = new() { ["en"] = "HR HELPLINE", ["bn"] = "এইচআর হেল্পলাইন" },
        ["career.form.name"] = new() { ["en"] = "Full Name", ["bn"] = "পূর্ণ নাম" },
        ["career.form.phone"] = new() { ["en"] = "Phone Number", ["bn"] = "ফোন নম্বর" },
        ["career.form.email"] = new() { ["en"] = "Email Address", ["bn"] = "ইমেইল ঠিকানা" },
        ["career.form.address"] = new() { ["en"] = "Present Address", ["bn"] = "বর্তমান ঠিকানা" },
        ["career.form.education"] = new() { ["en"] = "Education Qualification", ["bn"] = "শিক্ষাগত যোগ্যতা" },
        ["career.form.experience"] = new() { ["en"] = "Experience (years)", ["bn"] = "অভিজ্ঞতা (বছর)" },
        ["career.form.position"] = new() { ["en"] = "Position Applied For", ["bn"] = "পদের নাম" },
        ["career.form.cv"] = new() { ["en"] = "Upload CV (PDF/DOC, max 5MB)", ["bn"] = "সিভি আপলোড (PDF/DOC, সর্বোচ্চ ৫এমবি)" },
        ["career.form.submit"] = new() { ["en"] = "Submit Application", ["bn"] = "আবেদন জমা দিন" },
        ["career.form.selectPosition"] = new() { ["en"] = "— Select position —", ["bn"] = "— পদ নির্বাচন করুন —" },
        ["career.position.guard"] = new() { ["en"] = "Security Guard", ["bn"] = "নিরাপত্তা গার্ড" },
        ["career.position.armed"] = new() { ["en"] = "Armed Guard", ["bn"] = "সশস্ত্র গার্ড" },
        ["career.position.vip"] = new() { ["en"] = "VIP Protection Officer", ["bn"] = "ভিআইপি সুরক্ষা কর্মকর্তা" },
        ["career.position.supervisor"] = new() { ["en"] = "Field Supervisor", ["bn"] = "ফিল্ড সুপারভাইজার" },
        ["career.position.control"] = new() { ["en"] = "Control Room Operator", ["bn"] = "কন্ট্রোল রুম অপারেটর" },
        ["career.success"] = new() { ["en"] = "✓ Application submitted successfully! Our HR team will contact you within 7 working days.", ["bn"] = "✓ আবেদন সফলভাবে জমা হয়েছে! আমাদের এইচআর টিম ৭ কর্মদিবসের মধ্যে আপনার সাথে যোগাযোগ করবে।" },

        // ============ CONTACT ============
        ["contact.label"] = new() { ["en"] = "CONTACT US", ["bn"] = "যোগাযোগ করুন" },
        ["contact.title"] = new() { ["en"] = "Get In Touch", ["bn"] = "যোগাযোগ করুন" },
        ["contact.sub"] = new()
        {
            ["en"] = "Reach out for quotes, partnerships, or any security inquiry — we respond within 24 hours.",
            ["bn"] = "কোটেশন, অংশীদারিত্ব, বা যেকোনো নিরাপত্তা বিষয়ে যোগাযোগ করুন — আমরা ২৪ ঘন্টার মধ্যে সাড়া দিই।"
        },
        ["contact.info.address"] = new() { ["en"] = "Office Address", ["bn"] = "অফিস ঠিকানা" },
        ["contact.info.addressVal"] = new() { ["en"] = "Level 6, Plot 37, Gulshan Avenue, Gulshan-1, Dhaka 1212, Bangladesh", ["bn"] = "লেভেল ৬, প্লট ৩৭, গুলশান এভিনিউ, গুলশান-১, ঢাকা ১২১২, বাংলাদেশ" },
        ["contact.info.phone"] = new() { ["en"] = "Phone", ["bn"] = "ফোন" },
        ["contact.info.email"] = new() { ["en"] = "Email", ["bn"] = "ইমেইল" },
        ["contact.info.whatsapp"] = new() { ["en"] = "WhatsApp", ["bn"] = "হোয়াটসঅ্যাপ" },
        ["contact.info.hours"] = new() { ["en"] = "Business Hours", ["bn"] = "কার্য সময়" },
        ["contact.info.hoursVal"] = new() { ["en"] = "Sat – Thu: 9:00 AM – 6:00 PM (Control room: 24/7)", ["bn"] = "শনি – বৃহঃ: সকাল ৯টা – সন্ধ্যা ৬টা (কন্ট্রোল রুম: ২৪/৭)" },
        ["contact.info.license"] = new() { ["en"] = "License", ["bn"] = "লাইসেন্স" },
        ["contact.info.licenseVal"] = new() { ["en"] = "Licensed by Govt. of Bangladesh", ["bn"] = "বাংলাদেশ সরকার কর্তৃক লাইসেন্সপ্রাপ্ত" },
        ["contact.form.title"] = new() { ["en"] = "Send us a message", ["bn"] = "আমাদের একটি বার্তা পাঠান" },
        ["contact.form.reply"] = new() { ["en"] = "We reply within 24 hours.", ["bn"] = "আমরা ২৪ ঘন্টার মধ্যে উত্তর দিই।" },
        ["contact.form.name"] = new() { ["en"] = "Your Name", ["bn"] = "আপনার নাম" },
        ["contact.form.company"] = new() { ["en"] = "Company (optional)", ["bn"] = "কোম্পানি (ঐচ্ছিক)" },
        ["contact.form.phone"] = new() { ["en"] = "Phone", ["bn"] = "ফোন" },
        ["contact.form.email"] = new() { ["en"] = "Email", ["bn"] = "ইমেইল" },
        ["contact.form.subject"] = new() { ["en"] = "Subject", ["bn"] = "বিষয়" },
        ["contact.form.message"] = new() { ["en"] = "Message", ["bn"] = "বার্তা" },
        ["contact.form.send"] = new() { ["en"] = "Send Email", ["bn"] = "ইমেইল পাঠান" },
        ["contact.form.clear"] = new() { ["en"] = "Clear", ["bn"] = "মুছুন" },
        ["contact.form.recaptcha"] = new() { ["en"] = "🛡️ Protected by Google reCAPTCHA", ["bn"] = "🛡️ Google reCAPTCHA দ্বারা সুরক্ষিত" },
        ["contact.success"] = new() { ["en"] = "✓ Message sent! Thank you for contacting us. We'll respond within 24 hours.", ["bn"] = "✓ বার্তা পাঠানো হয়েছে! যোগাযোগ করার জন্য ধন্যবাদ। আমরা ২৪ ঘন্টার মধ্যে উত্তর দেব।" },

        // ============ FOOTER ============
        ["footer.ctaTitle"] = new() { ["en"] = "Secure Your Business with USS Security", ["bn"] = "ইউএসএস সিকিউরিটির সাথে আপনার ব্যবসা সুরক্ষিত করুন" },
        ["footer.ctaSub"] = new() { ["en"] = "Get a free site assessment & quotation within 24 hours. Nationwide coverage across all 64 districts of Bangladesh.", ["bn"] = "২৪ ঘন্টার মধ্যে ফ্রি সাইট মূল্যায়ন ও কোটেশন পান। বাংলাদেশের সব ৬৪ জেলায় সারাদেশীয় কভারেজ।" },
        ["footer.ctaButton"] = new() { ["en"] = "Get Free Quotation", ["bn"] = "ফ্রি কোটেশন নিন" },
        ["footer.about"] = new()
        {
            ["en"] = "Licensed security personnel supplier protecting Bangladesh's leading industries, banks, hospitals, malls, factories and residential communities since 2010. Your Security, Our Commitment.",
            ["bn"] = "২০১০ সাল থেকে বাংলাদেশের শীর্ষস্থানীয় শিল্প, ব্যাংক, হাসপাতাল, মল, কারখানা ও আবাসিক সম্প্রদায়কে সুরক্ষিত করছি এমন লাইসেন্সপ্রাপ্ত নিরাপত্তা কর্মী সরবরাহকারী। আপনার নিরাপত্তা, আমাদের অঙ্গীকার।"
        },
        ["footer.quickLinks"] = new() { ["en"] = "Quick Links", ["bn"] = "দ্রুত লিংক" },
        ["footer.services"] = new() { ["en"] = "Services", ["bn"] = "সেবাসমূহ" },
        ["footer.contactUs"] = new() { ["en"] = "Contact Us", ["bn"] = "যোগাযোগ" },
        ["footer.newsletter"] = new() { ["en"] = "Newsletter", ["bn"] = "নিউজলেটার" },
        ["footer.rights"] = new() { ["en"] = "All rights reserved.", ["bn"] = "সর্বস্বত্ব সংরক্ষিত।" },
        ["footer.privacy"] = new() { ["en"] = "Privacy Policy", ["bn"] = "গোপনীয়তা নীতি" },
        ["footer.terms"] = new() { ["en"] = "Terms of Service", ["bn"] = "ব্যবহারের শর্তাবলী" },
    };
}
