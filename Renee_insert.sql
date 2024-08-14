SET IDENTITY_INSERT dbo.Categories ON;  
INSERT INTO Categories (Id, Name, Thumbnail, ThumbnailCate, Description, Status)
VALUES 
(1, 'Lips', 'https://www.reneecosmetics.in/cdn/shop/files/lips_Pink-Theme_250x250_5c612664-941f-4ac6-a94d-b86630a6e3f6.jpg?v=1710410205', 'https://www.reneecosmetics.in/cdn/shop/files/Lips_Category_Collection_banner_2c9050c4-a166-4524-a2d6-35325c0b613d.jpg?v=1708410393', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(2, 'Face', 'https://www.reneecosmetics.in/cdn/shop/files/Face_Pink-Theme_250x250_1.jpg?v=1710410965', 'https://www.reneecosmetics.in/cdn/shop/files/Face_Category_Collection_banner.jpg?v=1708410436', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(3, 'Eyes', 'https://www.reneecosmetics.in/cdn/shop/files/Eyes_Pink-Theme_250x250_4a19cb50-7c08-44b2-8670-6cab7dae1b78.jpg?v=1710411148', 'https://www.reneecosmetics.in/cdn/shop/files/Eyes_Category_Collection_banner.jpg?v=1708410384', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(4, 'Skin', 'https://www.reneecosmetics.in/cdn/shop/files/Skin_Pink-Theme_250x250_306049bd-6790-4464-8906-37c0874c0357.jpg?v=1717669497', 'https://www.reneecosmetics.in/cdn/shop/files/Tools_Category_Collection_banner.jpg?v=1708410497', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(5, 'Nails', 'https://www.reneecosmetics.in/cdn/shop/files/nails_Pink-Theme_250x250_9ac8e5e5-d214-4b08-83c2-04571dbabd10.jpg?v=1710410470', 'https://www.reneecosmetics.in/cdn/shop/files/Nails_Category_Collection_banner.jpg?v=1708410445', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(6, 'Fragrance', 'https://www.reneecosmetics.in/cdn/shop/files/Fragrance_Pink-Theme_250x250_171a8426-e318-497b-9816-6c880122a043.jpg?v=1710410647', 'https://www.reneecosmetics.in/cdn/shop/files/Fragrance_Category_Collection_banner.jpg?v=1708410454', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(7, 'Gifting', 'https://www.reneecosmetics.in/cdn/shop/files/Gifting_Pink-Theme_250x250_38e57bb2-c976-45e6-b170-150701f0d231.jpg?v=1710410646', 'https://www.reneecosmetics.in/cdn/shop/files/Gifting_Category_Collection_banner.jpg?v=1708410463', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1),
(8, 'Combo', 'https://www.reneecosmetics.in/cdn/shop/files/Combos_Pink-Theme_250x250_6ae3c002-9f6b-4d9b-b0cc-2b76927b9e8b.jpg?v=1710411065', 'https://www.reneecosmetics.in/cdn/shop/files/Combos_Category_Collection_banner.jpg?v=1708410426', 'Lorem ipsum odor amet, consectetuer adipiscing elit.', 1);

SET IDENTITY_INSERT dbo.Categories OFF;  

GO
INSERT INTO Payments (
    [Method], 
    [Description], 
    [Status]
)
VALUES
    ('Card', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Augue mauris diam finibus.', 1),
    ('COD', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Augue mauris diam finibus.', 1); 

GO
SET IDENTITY_INSERT dbo.Attributes ON;
INSERT INTO Attributes (Id, Name, Status)
VALUES 
(1, 'Size', 0),
(2, 'Color', 0),
(3, 'Packet', 0);
SET IDENTITY_INSERT dbo.Attributes OFF;

GO
SET IDENTITY_INSERT dbo.AttributeValues ON;
INSERT INTO AttributeValues (Id, Value, AttributeId, Status)
VALUES 
(1, '1', 3, 0),
(2, 'Pack of 4', 2, 0),
(3, 'Rouge', 2, 0),
(4, 'Plum', 2, 0),
(5, 'Petal', 2, 0),
(6, 'Gold', 2, 0),
(7, 'Rose Gold', 2, 0),
(8, '1', 3, 0),
(9, '1', 3, 0),
(10, 'Black', 2, 0),
(11, 'Brown', 2, 0),
(12, '1', 3, 0),
(13, '1', 3, 0),
(14, 'N03 Red Parade', 2, 0),
(15, 'N04 Vacay Vibes', 2, 0),
(16, 'Turquoise Hue', 2, 0),
(17, 'Silver Confetti', 2, 0),
(18, '50ml', 1, 0),
(19, '15ml', 1, 0),
(20, '50ml', 1, 0),
(21, '15ml', 1, 0),
(22, 'Fabness Combo', 2, 0),
(23, 'Fabness Combo Nude', 2, 0),
(24, 'Juicy Berries', 2, 0),
(25, 'Nutty Nudes', 2, 0),
(26, '1', 3, 0),
(27, '1', 3, 0),
(28, '3X Black', 2, 0),
(29, 'Extra Teal', 2, 0),
(30, 'Max Pink', 2, 0);
SET IDENTITY_INSERT dbo.AttributeValues OFF;

GO
SET IDENTITY_INSERT dbo.Products ON;
INSERT INTO Products
    ([Id], [Name], [Thumbnail], [CategoryId], [TotalQuantity], [OriginalPrice], [DiscountPrice], [Description], [Ingredients], [Guideline], [AdditionalInfo], [Status], [CreatedAt], [UpdatedAt])
VALUES
    (1, 'RENEE Madness pH Lipstick, 3gm', 'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-1.jpg?v=1687446357', 1, 100, 499.00, 499.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 22:09:07.8257487', '2024-08-14 22:09:07.8257679'),
    (2, 'RENEE Very Matte Lipsticks - Long Lasting Weightless Velvety Formula', 'https://www.reneecosmetics.in/cdn/shop/files/VeryMatteLipstickCombo_ListingPI_01.jpg?v=1688392446', 1, 400, 199.00, 199.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 22:54:13.9817921', '2024-08-14 22:54:13.9817946'),
    (3, 'RENEE Face Gloss with Hyaluronic Acid, 10ml', 'https://www.reneecosmetics.in/cdn/shop/files/renee-face-gloss-with-hyaluronic-acid-10ml-renee-cosmetics-6.jpg?v=1687446371', 2, 60, 599.00, 599.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 23:02:53.7234970', '2024-08-14 23:02:53.7234999'),
    (4, 'RENEE Makeup Fix Setting Spray, 60ml', 'https://www.reneecosmetics.in/cdn/shop/files/renee-makeup-fix-setting-spray-60ml-renee-cosmetics-1.jpg?v=1687788605', 2, 10, 399.00, 399.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 23:05:32.4925369', '2024-08-14 23:05:32.4925394'),
    (5, 'RENEE Full Volume 2-in-1 Mascara with Primer, 10ml', 'https://www.reneecosmetics.in/cdn/shop/files/FullVolume2in1Mascara_ListingPI_01.jpg?v=1688561243', 3, 30, 599.00, 599.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 23:15:17.0667864', '2024-08-14 23:15:17.0667900'),
    (6, 'RENEE Browfill Eyebrow Pen, 1 Ml', 'https://www.reneecosmetics.in/cdn/shop/files/BROWFILLPen_BLACK_ListingPI_BLACK.jpg?v=1709197402', 3, 60, 450.00, 450.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 23:21:51.7809030', '2024-08-14 23:21:51.7809060'),
    (7, 'RENEE Rice Water & 10% Niacinamide Serum, 30ml', 'https://www.reneecosmetics.in/cdn/shop/files/Rice-Water-_-Niacinamide-Serum_01.jpg?v=1714038560', 4, 30, 499.00, 499.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 23:26:02.9722382', '2024-08-14 23:26:02.9722413'),
    (8, 'RENEE Snail 99 Mucin Serum 50ml', 'https://www.reneecosmetics.in/cdn/shop/files/Snail-99-Mucin-Serum_Listing-PI_01_28358016-e141-488d-99ab-72d2ea4ec4ef.jpg?v=1717393694', 4, 30, 750.00, 750.00, NULL, NULL, NULL, NULL, 0, '2024-08-14 23:27:44.2874983', '2024-08-14 23:27:44.2875008'),
    (9, 'RENEE Gloss Touch Set of 4 Nail Enamels, 5ml each', 'https://www.reneecosmetics.in/cdn/shop/files/renee-gloss-touch-set-of-4-nail-enamels-5ml-each-renee-cosmetics-1.jpg?v=1687788605', 5, 60, 499.00, 499.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:05:07.6228317', '2024-08-15 00:05:07.6228348'),
    (10, 'RENEE Glitterati Nail Paint 10ml', 'https://www.reneecosmetics.in/cdn/shop/products/Glitterati_NailPaint_ListingPI_TurquoiseHue_01.jpg?v=1690885298', 5, 60, 149.00, 149.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:08:17.8891023', '2024-08-15 00:08:17.8891045'),
    (11, 'RENEE Red Noir Eau De Parfum 50ml', 'https://www.reneecosmetics.in/cdn/shop/files/Renee_Red_Noir_Perfume_Mock.jpg?v=1716270001', 6, 60, 350.00, 350.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:16:53.5740730', '2024-08-15 00:16:53.5740761'),
    (13, 'RENEE Eau De Parfum OUD Aspire', 'https://www.reneecosmetics.in/cdn/shop/files/renee-eau-de-parfum-oud-aspire-renee-cosmetics-1.png?v=1716270927', 6, 60, 350.00, 350.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:21:02.9852361', '2024-08-15 00:21:02.9852385'),
    (14, 'RENEE FABNESS COMBO', 'https://www.reneecosmetics.in/cdn/shop/files/renee-fabness-combo-renee-cosmetics-1.jpg?v=1687450126', 7, 60, 1249.00, 1249.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:25:18.4955317', '2024-08-15 00:25:18.4955346'),
    (15, 'RENEE Stay With Me Matte Liquid Lipsticks Combo of 4, 5ml each', 'https://www.reneecosmetics.in/cdn/shop/files/renee-stay-with-me-matte-liquid-lipsticks-combo-of-4-5ml-each-renee-cosmetics-1.jpg?v=1687794015', 7, 60, 1999.00, 1999.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:27:37.1848190', '2024-08-15 00:27:37.1848308'),
    (17, 'RENEE Dream Kit', 'https://www.reneecosmetics.in/cdn/shop/files/Dream-Kit_Listing-PI_02.jpg?v=1720430162', 8, 30, 2097.00, 2097.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:36:12.7384079', '2024-08-15 00:36:12.7384089'),
    (18, 'RENEE Madness PH Stick & Lady In Crystal Lip Gloss Combo', 'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-stick-and-lady-in-crystal-lip-gloss-combo-renee-cosmetics-1.jpg?v=1687781581', 8, 30, 849.00, 849.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:38:57.9461069', '2024-08-15 00:38:57.9461077'),
    (19, 'RENEE Extreme Stay Liquid Eyeliner 4.5ml', 'https://www.reneecosmetics.in/cdn/shop/files/renee-extreme-stay-liquid-eyeliner-4-5ml-renee-cosmetics-1.jpg?v=1687788218', 3, 90, 450.00, 450.00, NULL, NULL, NULL, NULL, 0, '2024-08-15 00:42:14.5831022', '2024-08-15 00:42:14.5831028');
SET IDENTITY_INSERT dbo.Products OFF;


GO
SET IDENTITY_INSERT dbo.ProductAttributes ON;
INSERT INTO ProductAttributes
    ([Id], [ProductID], [AttributeValueID], [AttributePrice], [AttributeDiscountPrice], [Stock], [Status])
VALUES
    (1, 1, 1, 0.00, 0.00, 100, 0),
    (2, 2, 2, 400.00, 400.00, 100, 0),
    (3, 2, 3, 0.00, 0.00, 100, 0),
    (4, 2, 4, 0.00, 0.00, 100, 0),
    (5, 2, 5, 0.00, 0.00, 100, 0),
    (6, 3, 6, 0.00, 0.00, 10, 0),
    (7, 3, 7, 0.00, 0.00, 50, 0),
    (8, 4, 8, 0.00, 0.00, 10, 0),
    (9, 5, 9, 0.00, 0.00, 30, 0),
    (10, 6, 10, 0.00, 0.00, 30, 0),
    (11, 6, 11, 0.00, 0.00, 30, 0),
    (12, 7, 12, 0.00, 0.00, 30, 0),
    (13, 8, 13, 0.00, 0.00, 30, 0),
    (14, 9, 14, 0.00, 0.00, 30, 0),
    (15, 9, 15, 0.00, 0.00, 30, 0),
    (16, 10, 16, 0.00, 0.00, 30, 0),
    (17, 10, 17, 2.00, 2.00, 30, 0),
    (18, 11, 18, 249.00, 249.00, 30, 0),
    (19, 11, 19, 0.00, 0.00, 30, 0),
    (20, 13, 20, 249.00, 249.00, 30, 0),
    (21, 13, 21, 0.00, 0.00, 30, 0),
    (22, 14, 22, 0.00, 0.00, 30, 0),
    (23, 14, 23, 0.00, 0.00, 30, 0),
    (24, 15, 24, 0.00, 0.00, 30, 0),
    (25, 15, 25, 0.00, 0.00, 30, 0),
    (26, 17, 26, 0.00, 0.00, 30, 0),
    (27, 18, 27, 0.00, 0.00, 30, 0),
    (28, 19, 28, 1.00, 1.00, 30, 0),
    (29, 19, 29, 2.00, 2.00, 30, 0),
    (30, 19, 30, 3.00, 3.00, 30, 0);
SET IDENTITY_INSERT dbo.ProductAttributes OFF;


