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
            await db.Database.MigrateAsync(ct);
            
            List<Task> seedList = new List<Task>()
            {
                SeedCategory(db, ct),
                SeedProduct(db,ct)
            };
            await Task.WhenAll(seedList);
            
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
                
                await db.Products.AddRangeAsync(cpuProducts
                    .Concat(gpuProducts)
                    .Concat(motherboardsProduct)
                    .Concat(ramProducts)
                    .Concat(headphoneProducts)
                    .Concat(monitorProducts)
                    .Concat(casesProducts), ct);
                await db.SaveChangesAsync(ct);
            }
        }
    }
}
