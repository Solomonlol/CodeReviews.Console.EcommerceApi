using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Seeding
{
    public static class SeedDb
    {
        
        public static async Task SeedAll(this WebApplication app, CancellationToken ct = default)
        {
            using var scope = app.Services.CreateAsyncScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
            await db.Database.EnsureDeletedAsync(ct);
            await db.Database.MigrateAsync(ct);

            await SeedCategory(db, ct);
            await SeedProduct(db, ct);
            await SeedUser(db, hasher, ct);
        }

        private static async Task SeedCategory(ApplicationContext db, CancellationToken ct)
        {
            if(!await db.Categories.AnyAsync(ct))
            {
                var categories = new List<Category>
                {
                    new() { Name="CPU", Description="Category for personal computer processors"},
                    new() { Name="GPU", Description="Category for personal computer discrete vodeoadapters"},
                    new() { Name="Cases", Description="Category for personal computer cases"},
                    new() { Name="RAM", Description="Category for personal computer memory modules"},
                    new() { Name="Monitors", Description="Category for monitors"},
                    new() { Name="Headphones", Description="Category for headphones"},
                    new() { Name="Motherboards", Description="Category for personal computer motherboardes"},
                    new() { Name="Fans", Description="Category for personal computer cooling systems"},
                    new() { Name="Mouse", Description="Category for personal computer mouses"},
                    new() { Name="Keyboards", Description="Category for personal computer keyboardes"},
                    new() { Name="Laptops", Description="Category for laptops"},
                    new() { Name="SSD", Description="Category for SSD"},
                    new() { Name="HDD", Description="Category for HDD"},
                };

                await db.Categories.AddRangeAsync(categories, ct);
                await db.SaveChangesAsync(ct);
            }
        }

        private static async Task SeedUser(ApplicationContext db, IPasswordHasher<User> passwordHasher, CancellationToken ct)
        {
            var password = "Password123";
            if (!await db.Users.AnyAsync(ct))
            {
                var users = new List<User>()
                {
                    new() { Login="First", FirstName="Alexey", LastName="Gorin", PhoneNumber = "+123 12 1234567" },
                    new() { Login="Second", FirstName="Natalia", LastName="Nekrasova", PhoneNumber = "+123 12 2234567" },
                    new() { Login="Third", FirstName="Vasiliy", LastName="Gromov", PhoneNumber = "+123 12 3234567" },
                    new() { Login="Fourth", FirstName="Evgeniy", LastName="Petrov", PhoneNumber = "+123 12 4234567" },
                    new() { Login="Fifth", FirstName="Alexander", LastName="Pushnoy", PhoneNumber = "+123 12 5234567" },
                };
                foreach (var user in users)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                    user.Email = $"{user.Login}" + "@gmail.com";
                }

                await db.Users.AddRangeAsync(users, ct);
                await db.SaveChangesAsync(ct);
            }
        }

        private static async Task SeedProduct(ApplicationContext db, CancellationToken ct)
        {
            if(!await db.Products.AnyAsync(ct))
            {
                var cpuProducts = new List<Product>
                {
                    new() { Name="AMD Ryzen 7 7800X3D",
                        CategoryId=1,
                        Description="The processor demonstrates good energy efficiency and moderate heat output, even under heavy load.",
                        IsDeleted=false, Price = 300m },
                    new() { Name="AMD Ryzen 5 5600",
                        CategoryId=1,
                        Description="High performance in games and tasks thanks to 6 cores and 12 threads.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="AMD Ryzen 5 7500F",
                        CategoryId=1,
                        Description="6 cores and 12 threads based on the Zen 4 architecture deliver excellent performance in games and tasks.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="Intel Core i5-12400F",
                        CategoryId=1,
                        Description="Take advantage of the latest platform technologies that deliver incredible capabilities for gaming, work, and creativity.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="Intel Core i5-14600KF",
                        CategoryId=1,
                        Description=string.Empty,
                        IsDeleted=false, Price = 300m },
                    new() { Name="AMD Ryzen 9 9950X3D",
                        CategoryId=1,
                        Description="High performance in gaming and creative tasks thanks to 3D V-Cache and 16 Zen 5 cores.",
                        IsDeleted=false, Price = 700m },
                    new() { Name="Intel Core Ultra 7 270K Plus",
                        CategoryId=1,
                        Description="A high-performance, next-generation desktop processor designed for gaming PCs and powerful workstations.",
                        IsDeleted=false, Price = 400m },
                    new() { Name="AMD Ryzen 7 9850X3D",
                        CategoryId=1,
                        Description=string.Empty,
                        IsDeleted=false, Price = 500m },
                };
                var gpuProducts = new List<Product>
                {
                    new() { Name="ASUS Dual GeForce RTX 5070 12GB GDDR7 OC Edition", 
                        CategoryId=2, 
                        Description="A powerful and compact solution for modern gaming machines and industrial systems.",
                        IsDeleted=false, Price = 1000m },
                    new() { Name="ASUS Prime Radeon RX 9070 XT OC Edition 16GB GDDR6",
                        CategoryId=2,
                        Description="High-performance graphics card; software developed for exceptional graphics performance in modern games and applications.",
                        IsDeleted=false, Price = 1000m },
                    new() { Name="ASUS ROG Astral LC GeForce RTX 5090 32GB GDDR7 OC Edition",
                        CategoryId=2,
                        Description="A unique 360x38 mm radiator, combined with high-profile fans, ensures superior heat dissipation.",
                        IsDeleted=false, Price = 5000m },
                    new() { Name="ASUS Dual GeForce RTX 5060 8GB GDDR7 OC Edition",
                        CategoryId=2,
                        Description="Thanks to its compact 2.5-slot form factor and proprietary dual-fan Axial-tech cooling system, the card delivers powerful graphics performance even in small-form-factor cases.",
                        IsDeleted=false, Price = 550m },
                    new() { Name="ASUS ROG Astral GeForce RTX 5080 16GB GDDR7 OC Edition",
                        CategoryId=2,
                        Description="Inspired by the wonders of the universe, this graphics card blends art and technology, delivering outstanding cooling and unmatched performance.",
                        IsDeleted=false, Price = 2500m },
                    new() { Name="ASUS Prime Radeon RX 9060 XT OC Edition 16GB GDDR6",
                        CategoryId=2,
                        Description="A powerful solution for gamers and professionals seeking high performance and reliability.",
                        IsDeleted=false, Price = 700m }
                };
                var casesProducts = new List<Product>
                {
                    new() { Name="Zalman P30 Air",
                        CategoryId=3,
                        Description="Perfect for building a powerful gaming system or running resource-intensive applications.",
                        IsDeleted=false, Price = 80m },
                    new() { Name="Ocypus Gamma C72 BK ARGB",
                        CategoryId=3,
                        Description="The case supports ATX, micro-ATX, and mini-ITX motherboards, making it an excellent choice for building both high-performance and compact systems.",
                        IsDeleted=false, Price = 60m },
                    new() { Name="DeepCool Matrexx 50",
                        CategoryId=3,
                        Description=string.Empty,
                        IsDeleted=false, Price = 50m },
                    new() { Name="DeepCool CH560 Digital Black",
                        CategoryId=3,
                        Description="Three vibrant 140mm ARGB PWM fans mounted in front of the main compartment, a hybrid side panel, and a dual-status digital display set the CH560 DIGITAL apart from the competition.",
                        IsDeleted=false, Price = 100m },
                    new() { Name="Lian Li O11 Dynamic Mini V2 Flow",
                        CategoryId=3,
                        Description="A stylish and compact Mid-Tower case designed for high-performance builds, with full support for ATX motherboards.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="Lian Li Lancool 216 ARGB",
                        CategoryId=3,
                        Description=string.Empty,
                        IsDeleted=false, Price = 120m },
                    new() { Name="Lian Li Lancool III",
                        CategoryId=3,
                        Description="Magnetic dust filters are provided at the front, top, and left sides to ensure a higher level of dust filtration. Please note that the dust filters will affect airflow.",
                        IsDeleted=false, Price = 170m },
                };
                var ramProducts = new List<Product>
                {
                    new() { Name="ADATA XPG Lancer Blade RGB 2x16ГБ DDR5 6000",
                        CategoryId=4,
                        Description="The ADATA XPG Lancer Blade RGB memory comes as a kit of two 16GB modules.",
                        IsDeleted=false, Price = 700m },
                    new() { Name="G.Skill Trident Z5 Neo RGB 2x64ГБ DDR5 6000",
                        CategoryId=4,
                        Description=string.Empty,
                        IsDeleted=false, Price = 2500m },
                    new() { Name="Team T-Create Expert 2x64ГБ DDR5 6400",
                        CategoryId=4,
                        Description=string.Empty,
                        IsDeleted=false, Price = 3000m },
                    new() { Name="Kingston FURY Beast 2x8GB DDR4 PC4-25600",
                        CategoryId=4,
                        Description="This is a kit consisting of two 8 GB (1G x 64-bit) DDR4-3200 CL16 SDRAM memory modules, built using eight 1G x 8-bit FBGA chips. The total capacity of the kit is 16 GB. Each module supports Intel Extreme Memory Profiles (Intel XMP) 2.0.",
                        IsDeleted=false, Price = 400m },
                };
                var monitorProducts = new List<Product>
                {
                    new() { Name="Xiaomi 2K Gaming Monitor G27Qi 2026",
                        CategoryId=5,
                        Description="A high refresh rate and a fast 1ms GtG response time ensure smooth in-game motion without blur or lag. With DisplayHDR 400 certification and precise color reproduction (Delta E < 2), the monitor stands out from typical gaming models thanks to its advanced color capabilities.",
                        IsDeleted=false, Price = 200m },
                    new() { Name="Samsung Odyssey OLED G6",
                        CategoryId=5,
                        Description="The monitor features a 27-inch QD-OLED panel with a 2560x1440 resolution, a 240Hz refresh rate, and an ultra-fast 0.03ms GtG response time. With support for HDR10 and AMD FreeSync Premium technology, it is ideal for fast-paced gaming and work involving rich visual effects.",
                        IsDeleted=false, Price = 700m },
                    new() { Name="AOC Q27B3MA",
                        CategoryId=5,
                        Description="27\", 2560x1440, 16:9, VA, 75 Hz, 250 nits brightness, speakers, HDMI+DisplayPort",
                        IsDeleted=false, Price = 120m },
                    new() { Name="LG UltraGear 27G411A-B",
                        CategoryId=5,
                        Description="This 27-inch gaming monitor with 1920x1080 resolution is ideal for fast-paced games, thanks to its 144Hz refresh rate and 5ms GtG response time. The IPS panel delivers rich colors and wide 178-degree viewing angles. Support for HDR10 and 99% sRGB color coverage ensures a vivid, realistic image.",
                        IsDeleted=false, Price = 110m },
                    new() { Name="Gigabyte GO27Q24A",
                        CategoryId=5,
                        Description="A 27-inch gaming monitor featuring QHD resolution (2560x1440) and an advanced Samsung QD-OLED panel that delivers vivid, saturated colors and a contrast ratio of up to 1.5M:1. True 10-bit color reproduction and 99% DCI-P3 coverage create cinematic image quality with a rich palette and precise tonal gradation, while a Delta E < 2 calibration ensures high color accuracy right out of the box. This level of visual performance is particularly important for demanding gamers and content creators working with graphics and video.",
                        IsDeleted=false, Price = 110m },
                };
                var headphoneProducts = new List<Product>
                {
                    new() { Name="Anker Soundcore Space 2",
                        CategoryId=6,
                        Description="Full-sized Bluetooth headphones featuring a microphone, Hi-Res Audio support, and active noise cancellation. They are equipped with 40mm dynamic drivers and a high-capacity 750 mAh battery, delivering up to 70 hours of playback without ANC. Weighing 264 grams, they ensure comfort with synthetic leather and foam ear pads.",
                        IsDeleted=false, Price = 130m },
                    new() { Name="Xiaomi Redmi Headphones Neo",
                        CategoryId=6,
                        Description="These are full-sized wireless headphones featuring active noise cancellation of up to 42 dB, adaptive ANC modes, and an AI-powered triple-microphone system for crystal-clear voice transmission. Three microphones work in tandem with AI algorithms to precisely separate speech from background noise, while the wind noise suppression system effectively handles wind speeds of up to 5 m/s, ensuring clear calls even outdoors or while on the move.",
                        IsDeleted=false, Price = 80m },
                    new() { Name="Razer Opus X",
                        CategoryId=6,
                        Description="Thanks to internal microphones designed for voice communication, the Opus X ensures that you always hear all conference call participants—and they hear you—with perfect clarity.",
                        IsDeleted=false, Price = 90m },
                    new() { Name="Marshall Monitor III ANC",
                        CategoryId=6,
                        Description="70 HOURS OF WIRELESS PLAYTIME WITH ACTIVE NOISE CANCELLATION: 70 hours of wireless playtime with active noise cancellation. 100 hours without it. Endless hours of explosive listening.",
                        IsDeleted=false, Price = 250m },
                    new() { Name="Apple AirPods Pro 3",
                        CategoryId=6,
                        Description="The AirPods Pro 3 feature an upgraded active noise cancellation system that performs significantly better than previous versions. It allows you to completely block out external noise, ensuring total immersion in music, podcasts, or calls. This level of isolation makes the earbuds comfortable to use even in the noisiest environments.",
                        IsDeleted=false, Price = 250m },
                };
                var motherboardsProduct = new List<Product>
                {
                    new() { Name="Sapphire Pure B850M WiFi",
                        CategoryId=7,
                        Description="A microATX motherboard supporting AMD Ryzen 9000, 8000, and 7000 series processors on the AM5 socket. It features the AMD B850 chipset, four DDR5 slots, and support for up to 192GB of RAM at speeds up to 8000 MHz (OC). Connectivity options include PCIe 5.0 x16, two M.2 slots (one PCIe 5.0, one PCIe 4.0), four SATA 3.0 ports, Wi-Fi 6, and Bluetooth 5.3.",
                        IsDeleted=false, Price = 190m },
                    new() { Name="ASRock B650M-H/M.2+",
                        CategoryId=7,
                        Description="The 6-layer PCB ensures stable signal transmission, low temperatures, and reliable system-wide operation.",
                        IsDeleted=false, Price = 100m },
                    new() { Name="MSI B550M Pro-VDH WiFi",
                        CategoryId=7,
                        Description="Durable construction with increased copper content and high-quality components.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="Gigabyte B550 Gaming X V2 (rev. 1.0/1.1/1.2)",
                        CategoryId=7,
                        Description="A 13-phase power delivery system with heatsinks ensures stability under load.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="ASUS TUF Gaming B850-Plus WiFi",
                        CategoryId=7,
                        Description="The ATX-format ASUS TUF GAMING B850-PLUS WIFI motherboard is designed for building high-performance systems based on the latest AMD processors. It features the AM5 socket and supports Ryzen 9000, 8000, and 7000 series processors, offering extensive compatibility and upgrade options.",
                        IsDeleted=false, Price = 280m },
                    new() { Name="ASUS ROG Strix X870E-E Gaming WiFi",
                        CategoryId=7,
                        Description="The ROG STRIX X870E-E GAMING WIFI motherboard combines cutting-edge technologies, including support for AMD Ryzen 9000, 8000, and 7000 series processors using the AM5 socket. It features the AMD X870E chipset, delivering top-tier performance and stability for gaming systems and workstations.",
                        IsDeleted=false, Price = 700m },
                    new() { Name="ASRock X870E Taichi",
                        CategoryId=7,
                        Description="Supports AMD Ryzen 9000, 8000, and 7000 series processors. 24+2+1 power phases, 110A SPS. Supports dual-channel mode, up to 8200+ (OC).",
                        IsDeleted=false, Price = 700m },
                };
                var fanProducts = new List<Product>
                {
                    new() { Name="Zalman CNPS13X Black",
                        CategoryId=8,
                        Description="Unlike traditional cooling fans, the specially designed Zalman AF120 Annular Fan—featuring \"shark fin\" blades—minimizes airflow resistance, thereby reducing operating noise. Its hydrodynamic bearing operates without metal-to-metal contact, utilizing a lubricating film instead of a traditional bearing mechanism. This design ensures low noise levels, low operating temperatures, and shock resistance, making it a highly reliable and durable component.",
                        IsDeleted=false, Price = 30m },
                    new() { Name="ADATA XPG Levante II 240",
                        CategoryId=8,
                        Description="A liquid CPU cooling system featuring a 240mm radiator, two 120mm fans, and support for modern Intel and AMD processors. It efficiently dissipates up to 300W of heat and is equipped with ARGB lighting and a quiet fluid dynamic bearing. Its 276mm radiator length and slim profile make it suitable for compact cases.",
                        IsDeleted=false, Price = 100m },
                    new() { Name="Arctic Liquid Freezer III Pro 360",
                        CategoryId=8,
                        Description="The ARCTIC Liquid Freezer III Pro 360 is a high-performance liquid CPU cooling system featuring a 398 mm radiator and three 120 mm P12 Pro fans. This new version comes with an improved 38 mm thick radiator, ensuring efficient cooling even under high thermal loads.",
                        IsDeleted=false, Price = 120m },
                    new() { Name="ID-Cooling DX360 Max Black",
                        CategoryId=8,
                        Description="The ID-COOLING DX360 MAX is a high-performance liquid cooling system for processors with a TDP of up to 350W. The model features a 400×120×38 mm aluminum radiator and three 120 mm fans, delivering a maximum airflow of up to 85 CFM with a noise level of up to 32.5 dB.",
                        IsDeleted=false, Price = 65m },
                    new() { Name="Noctua NH-D15 G2",
                        CategoryId=8,
                        Description="The Noctua NH-D15 G2 is the second generation of the iconic tower-style CPU cooler, featuring improvements over the original NH-D15 model. This new version is equipped with two upgraded NF-A14x25r G2 PWM fans, asymmetrical heatsink towers, and an increased number of heat pipes, delivering even more efficient and quiet cooling—even for powerful modern processors.",
                        IsDeleted=false, Price = 150m },
                };
                var mouseProducts = new List<Product>
                {
                    new() { Name="Lenovo Yoga Pro",
                        CategoryId=9,
                        Description="A full-sized wireless mouse designed for comfort and style, featuring the signature Yoga series aesthetic. Its ergonomic body, with soft-touch side grips, ensures comfortable use even during extended work sessions.",
                        IsDeleted=false, Price = 40m },
                    new() { Name="Logitech M350 Pebble",
                        CategoryId=9,
                        Description=string.Empty,
                        IsDeleted=false, Price = 15m },
                    new() { Name="A4Tech Bloody V8M Max",
                        CategoryId=9,
                        Description="Equipped with a professional-grade sensor featuring 12,000 CPI, 8,000 FPS, 250 IPS, and 35G acceleration, the mouse delivers the precision and control needed for fast-paced gaming, such as FPS titles.",
                        IsDeleted=false, Price = 20m },
                    new() { Name="Logitech G304 Lightspeed",
                        CategoryId=9,
                        Description="The Logitech G304 wireless gaming mouse features LIGHTSPEED technology, delivering a 1ms response time that rivals wired solutions in speed.",
                        IsDeleted=false, Price = 30m },
                    new() { Name="Logitech Pro X Superlight 2",
                        CategoryId=9,
                        Description="High battery life; holds a charge for up to a month and a half with daily use.",
                        IsDeleted=false, Price = 140m },
                    new() { Name="Razer Naga V2 Pro",
                        CategoryId=9,
                        Description="Full-size gaming mouse; wired (USB Type-C), wireless (Bluetooth), and wireless (RF) connectivity; 30,000 DPI optical sensor; 9 buttons; tilt-click scroll wheel; black.",
                        IsDeleted=false, Price = 280m },
                };
                var keyboardProducts = new List<Product>
                {
                    new() { Name="Keychron K10 HE Standart Version Black K10H-J1-RU (Nebula Magnetic)",
                        CategoryId=9,
                        Description="Standard, magnetic, Gateron Magnetic Nebula 2.0, linear travel, all-metal, connectivity: USB-A/wireless (USB-A)/Bluetooth, backlight, with Cyrillic characters, black.",
                        IsDeleted=false, Price = 200m },
                    new() { Name="Lenovo Legion K310 RGB",
                        CategoryId=9,
                        Description="A full-sized gaming keyboard that offers the user all the features necessary for comfortable work and effective gaming. The keyboard is equipped with membrane switches that ensure quiet keystrokes and rapid response times, making it suitable for both long gaming sessions and office work.",
                        IsDeleted=false, Price = 40m },
                    new() { Name="Samsung Smart Keyboard",
                        CategoryId=9,
                        Description="Compact, scissor-switch, all-metal, Bluetooth connectivity, Cyrillic layout, black.",
                        IsDeleted=false, Price = 90m },
                    new() { Name="A4Tech KV-300H",
                        CategoryId=9,
                        Description="Standard, scissor-switch, plastic, USB-A connection, with Cyrillic characters, USB hub, gray.",
                        IsDeleted=false, Price = 30m },
                    new() { Name="Logitech Corded Keyboard K280e",
                        CategoryId=9,
                        Description="Multimedia, membrane, plastic, USB-A interface, with Cyrillic characters, moisture-resistant, black.",
                        IsDeleted=false, Price = 25m },
                    new() { Name="Razer Huntsman V2 Analog",
                        CategoryId=9,
                        Description="Gaming, optical, Razer Analog Optical Switch, metal top plate, USB-A interface, lighting, no Cyrillic characters, USB hub, black",
                        IsDeleted=false, Price = 250m },
                };
                var laptopProducts = new List<Product>
                {
                    new() { Name="Lenovo Legion 5 15AHP10",
                        CategoryId=10,
                        Description="15.1\" 2560x1600, OLED, 165 Hz, AMD Ryzen 7 260, 16 GB DDR5, 512 GB SSD, NVIDIA GeForce RTX 5060 8 GB (TGP 115 W), no OS, black lid, 80 Wh battery",
                        IsDeleted=false, Price = 2000m },
                    new() { Name="Lenovo IdeaPad Slim 3 15IRH10",
                        CategoryId=10,
                        Description="15.3\" 1920x1200, IPS, 60 Hz, Intel Core i7-13620H, 16 GB DDR5, 1024 GB SSD, no OS, gray lid, 50 Wh battery",
                        IsDeleted=false, Price = 1000m },
                    new() { Name="ASUS ROG Strix G16 2025 G614FR-S5022W",
                        CategoryId=10,
                        Description="A powerful gaming laptop designed for modern games, streaming, and resource-intensive creative tasks. Higher-end configurations feature an AMD Ryzen 9 processor and an NVIDIA GeForce RTX 5080 laptop GPU, and run on Windows 11 Pro.",
                        IsDeleted=false, Price = 3000m },
                    new() { Name="Acer Aspire Lite 16 AL16-54P-52AL",
                        CategoryId=10,
                        Description="16.0\" 1920x1200, IPS, 60 Hz, Intel Core 5 120U, 16 GB DDR5, 512 GB SSD, Windows 11 Home, silver lid, 58 Wh battery",
                        IsDeleted=false, Price = 900m },
                    new() { Name="Apple MacBook Neo 13\" A18 Pro 2026 MHFH4",
                        CategoryId=10,
                        Description="A compact and affordable Apple laptop designed for everyday work, study, and multimedia. It features a 13-inch Liquid Retina display with a resolution of 2408 x 1506 pixels, brightness of up to 500 nits, and support for 1 billion colors, ensuring crisp images and natural color reproduction when working with text, photos, and video. Its lightweight aluminum body weighs approximately 1.23 kg, making it convenient to carry around.",
                        IsDeleted=false, Price = 900m },
                    new() { Name="Acer Nitro V 15 ANV15-52-57BB",
                        CategoryId=10,
                        Description="15.6\" 1920x1080, IPS, 165 Hz, Intel Core i5-13420H, 16 GB DDR4, 512 GB SSD, NVIDIA GeForce RTX 5050 8 GB (TGP 75 W), Windows 11 Home, black lid, 76 Wh battery",
                        IsDeleted=false, Price = 1200m },
                };
                var ssdProducts = new List<Product>
                {
                    new() { Name="ADATA Legend 900 Pro 1TB SLEG-900P-1TCS",
                        CategoryId=11,
                        Description="The LEGEND 900 PRO is built on the modern PCIe Gen4 x4 interface and fully complies with the NVMe 1.4 standard, ensuring compatibility with the latest Intel and AMD platforms. This makes it an excellent tool for those working with graphics, 3D animation, and other resource-intensive tasks. The device is ideal for both professionals and enthusiasts looking to bring their most ambitious creative projects to life.",
                        IsDeleted=false, Price = 220m },
                    new() { Name="Patriot P300 512GB P300P512GM28",
                        CategoryId=11,
                        Description="512 GB, M.2 2280, PCI Express 3.0 x4, 3D TLC NAND chips, sequential access: 1700/1100 MB/s, random access: 290,000/260,000 IOPS",
                        IsDeleted=false, Price = 100m },
                    new() { Name="Kingston A400 240GB SA400S37/240G",
                        CategoryId=11,
                        Description="The SLC cache ensures high write speeds—until the buffer is exhausted—during everyday tasks.",
                        IsDeleted=false, Price = 80m },
                    new() { Name="Kingston KC3000 1TB SKC3000S/1024G",
                        CategoryId=11,
                        Description="1 TB, M.2 2280, PCI Express 4.0 x4, Phison PS5018-E18 controller, 3D TLC NAND chips, sequential access: 7000/6000 MB/s, random access: 900,000/1,000,000 IOPS, PS5 compatibility",
                        IsDeleted=false, Price = 300m },
                    new() { Name="Samsung 990 Pro 1TB MZ-V9P1T0BW",
                        CategoryId=11,
                        Description="1 TB, M.2 2280, PCI Express 4.0 x4, Samsung Pascal controller, 3D TLC NAND chips, sequential access: 7450/6900 MB/s, random access: 1,200,000/1,550,000 IOPS, PS5 compatibility",
                        IsDeleted=false, Price = 300m },
                    new() { Name="Samsung 9100 Pro 4TB MZ-VAP4T0BW",
                        CategoryId=11,
                        Description="4 TB, M.2 2280, PCI Express 5.0 x4, Samsung Presto S4LY027 controller, 3D TLC NAND chips, sequential access: 14,800/13,400 MB/s, random access: 2,200,000/2,600,000 IOPS, PS5 compatibility",
                        IsDeleted=false, Price = 1200m },
                };
                var hddProducts = new List<Product>
                {
                    new() { Name="WD Purple 1TB [WD10PURZ]",
                        CategoryId=12,
                        Description="3.5\", SATA 3.0 (6Gbps), 5400 rpm, 64 MB buffer, CMR technology, air-filled",
                        IsDeleted=false, Price = 150m },
                    new() { Name="WD Red Plus 4TB WD40EFPX",
                        CategoryId=12,
                        Description="3.5\", SATA 3.0 (6Gbps), 5400 rpm, 256 MB buffer, CMR technology",
                        IsDeleted=false, Price = 280m },
                    new() { Name="Seagate IronWolf 4TB ST4000VN006",
                        CategoryId=12,
                        Description="3.5\", SATA 3.0 (6Gbps), 5400 rpm, 256 MB buffer, CMR technology, air-filled",
                        IsDeleted=false, Price = 250m },
                    new() { Name="SWD Red Plus 8TB WD80EFPX",
                        CategoryId=12,
                        Description="3.5\", SATA 3.0 (6Gbps), 5640 rpm, 256 MB buffer, CMR technology, air-filled",
                        IsDeleted=false, Price = 500m },
                    new() { Name="WD Caviar Blue 1TB (WD10EZEX)",
                        CategoryId=12,
                        Description="3.5\", SATA 3.0 (6Gbps), 7200 rpm, 64 MB buffer, linear speed 150/150 MB/s, CMR technology",
                        IsDeleted=false, Price = 500m },
                    new() { Name="Seagate Barracuda 2TB ST2000DM008",
                        CategoryId=12,
                        Description="C3.5\", SATA 3.0 (6Gbps), 7200 rpm, 256 MB buffer, SMR technology",
                        IsDeleted=false, Price = 180m },
                };
                await db.Products.AddRangeAsync(cpuProducts
                    .Concat(gpuProducts)
                    .Concat(motherboardsProduct)
                    .Concat(ramProducts)
                    .Concat(headphoneProducts)
                    .Concat(monitorProducts)
                    .Concat(casesProducts)
                    .Concat(fanProducts)
                    .Concat(mouseProducts)
                    .Concat(keyboardProducts)
                    .Concat(laptopProducts)
                    .Concat(ssdProducts)
                    .Concat(hddProducts), ct);
                await db.SaveChangesAsync(ct);
            }
        }
    }
}
