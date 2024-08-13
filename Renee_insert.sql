-- SET IDENTITY_INSERT dbo.Categories ON;  
INSERT INTO dbo.Categories
    ([Name], [Thumbnail], [Description], [Status], [ThumbnailCate])
VALUES
    ('Lips', 'https://www.reneecosmetics.in/cdn/shop/files/lips_Pink-Theme_250x250_5c612664-941f-4ac6-a94d-b86630a6e3f6.jpg?v=1710410205', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Lips_Category_Collection_banner_2c9050c4-a166-4524-a2d6-35325c0b613d.jpg?v=1708410393'),
    ('Face', 'https://www.reneecosmetics.in/cdn/shop/files/Face_Pink-Theme_250x250_1.jpg?v=1710410965', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Face_Category_Collection_banner.jpg?v=1708410436'),
    ('Eyes', 'https://www.reneecosmetics.in/cdn/shop/files/Eyes_Pink-Theme_250x250_4a19cb50-7c08-44b2-8670-6cab7dae1b78.jpg?v=1710411148', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Eyes_Category_Collection_banner.jpg?v=1708410384'),
    ('Nails', 'https://www.reneecosmetics.in/cdn/shop/files/nails_Pink-Theme_250x250_9ac8e5e5-d214-4b08-83c2-04571dbabd10.jpg?v=1710410470', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Nails_Category_Collection_banner.jpg?v=1708410445'),
    ('Fragrance', 'https://www.reneecosmetics.in/cdn/shop/files/Fragrance_Pink-Theme_250x250_171a8426-e318-497b-9816-6c880122a043.jpg?v=1710410647', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Fragrance_Category_Collection_banner.jpg?v=1708410454'),
    ('Gifting', 'https://www.reneecosmetics.in/cdn/shop/files/Gifting_Pink-Theme_250x250_38e57bb2-c976-45e6-b170-150701f0d231.jpg?v=1710410646', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Gifting_Category_Collection_banner.jpg?v=1708410463'),
    ('Combo', 'https://www.reneecosmetics.in/cdn/shop/files/Combos_Pink-Theme_250x250_6ae3c002-9f6b-4d9b-b0cc-2b76927b9e8b.jpg?v=1710411065', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Combos_Category_Collection_banner.jpg?v=1708410426'),
    ('Skin', 'https://www.reneecosmetics.in/cdn/shop/files/Skin_Pink-Theme_250x250_306049bd-6790-4464-8906-37c0874c0357.jpg?v=1717669497', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.', 1, 'https://www.reneecosmetics.in/cdn/shop/files/Tools_Category_Collection_banner.jpg?v=1708410497');

-- SET IDENTITY_INSERT dbo.Categories OFF;  
GO
INSERT INTO Attributes ([Name], [Status])
VALUES
    ('Color', 0),
    ('Size', 0),
    ('Packet', 0);

GO
INSERT INTO Payments (
    [Method], 
    [Description], 
    [Status]
)
VALUES
    ('Card', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Augue mauris diam finibus.', 1),
    ('COD', 'Lorem ipsum odor amet, consectetuer adipiscing elit. Augue mauris diam finibus.', 1);    

INSERT INTO AttributeValues ([Value], [AttributeId], [Status])
VALUES
    ('Original', 1, 0),
    ('Fab 5', 1, 0),
    ('Fab 5 Nude', 1, 0),
    ('Clear', 1, 0),
    ('Pink', 1, 0),
    ('Nude', 1, 0),
    ('1', 3, 0),
    ('1', 3, 0),
    ('1', 3, 0),
    ('Earth 01', 1, 0),
    ('Saturn 02', 1, 0),
    ('Venus 03', 1, 0),
    ('4 in 1', 1, 0),
    ('Black', 1, 0),
    ('Brown', 1, 0),
    ('1', 3, 0),
    ('MTN 01', 1, 0),
    ('French Nails', 1, 0),
    ('BN 01', 1, 0),
    ('1', 3, 0),
    ('4', 3, 0),
    ('50 ML', 2, 0),
    ('15 ML', 2, 0),
    ('50 ML', 2, 0),
    ('15 ML', 2, 0),
    ('4', 3, 0);

INSERT INTO Products (
    [Name], 
    [Thumbnail], 
    [CategoryId], 
    [TotalQuantity], 
    [OriginalPrice], 
    [DiscountPrice], 
    [Description], 
    [Ingredients], 
    [Guideline], 
    [AdditionalInfo], 
    [Status], 
    [CreatedAt], 
    [UpdatedAt]
)
VALUES
    ('RENEE Madness pH Lipstick, 3gm', 'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-1.jpg?v=1687446357', 1, 100, 499.00, 499.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:07:34.4311511', '2024-08-12 10:07:34.4311536'),
    ('RENEE FAB 5 Matte Finish 5 in 1 Lipstick 7.5gm', 'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-1.jpg?v=1687441106', 1, 60, 750.00, 750.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:10:30.4500829', '2024-08-12 10:10:30.4500859'),
    ('RENEE Hot Lips - Lip Gloss 4.5ml', 'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_1.jpg?v=1715326888', 1, 90, 250.00, 250.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:14:40.7265442', '2024-08-12 10:14:40.7265505'),
    ('RENEE Madness pH Blush 3gm', 'https://www.reneecosmetics.in/cdn/shop/files/96A1E17C-10B6-4169-A5DF-3BA09B0F94AE.jpg?v=1695913838', 2, 30, 499.00, 499.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:18:32.5767301', '2024-08-12 10:18:32.5767327'),
    ('RENEE Lumi Glow Cream', 'https://www.reneecosmetics.in/cdn/shop/files/Lumi-Glow-Listing-PI_01.jpg?v=1718632923', 2, 30, 499.00, 499.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:20:10.9766623', '2024-08-12 10:20:10.9766643'),
    ('RENEE Immortal Face Cream 50 Gm', 'https://www.reneecosmetics.in/cdn/shop/files/Immortal_01_51cdb96c-28c5-4384-b02a-db7fce32d475.jpg?v=1705383325', 2, 30, 750.00, 750.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:22:07.1146907', '2024-08-12 10:22:07.1146937'),
    ('RENEE Holographic Eyeliner', 'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_001-min.jpg?v=1722503289', 3, 98, 499.00, 499.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:38:57.2343436', '2024-08-12 10:38:57.2343467'),
    ('RENEE Bold 4 4-IN-1 Kajal', 'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_01.jpg?v=1696685279', 3, 50, 599.00, 599.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:41:15.4164753', '2024-08-12 10:41:15.4164778'),
    ('RENEE Superdense Eyebrow Pencil, 0.4 gm', 'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_01-min.jpg?v=1713505434', 3, 109, 450.00, 450.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:43:13.9402990', '2024-08-12 10:43:13.9403014'),
    ('RENEE Nail Paint Remover, 100 Ml', 'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_1_-min.jpg?v=1713175558', 4, 50, 199.00, 199.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:46:03.2674986', '2024-08-12 10:46:03.2675001'),
    ('RENEE Stick On Nails', 'https://www.reneecosmetics.in/cdn/shop/files/renee-stick-on-nails-renee-cosmetics-56.jpg?v=1704374117', 4, 156, 499.00, 499.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:48:13.8601752', '2024-08-12 10:48:13.8601799'),
    ('RENEE Twist & Erase Nail Polish Remover 60ml', 'https://www.reneecosmetics.in/cdn/shop/files/renee-twist-and-erase-nail-polish-remover-60ml-renee-cosmetics-1.jpg?v=1687794015', 4, 51, 199.00, 199.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:50:12.4928165', '2024-08-12 10:50:12.4928187'),
    ('RENEE Eau De Parfum Pack of 4, 15ml each', 'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_01.jpg?v=1716182277', 5, 51, 999.00, 999.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:53:01.8019802', '2024-08-12 10:53:01.8019851'),
    ('RENEE Floral Fest Eau De Parfum', 'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_02-min.jpg?v=1716199577', 5, 102, 350.00, 350.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:55:39.3770578', '2024-08-12 10:55:39.3770585'),
    ('RENEE Citrus Blast Eau De Parfum', 'https://www.reneecosmetics.in/cdn/shop/files/CitrusBlast_02-min.jpg?v=1716199657', 5, 102, 350.00, 350.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 10:57:36.2243993', '2024-08-12 10:57:36.2243999'),
    ('RENEE Premium Fragrances Set Of 4, 15ml each', 'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-1_b456b203-0c0b-4fe5-bb7f-c901c18871bf.jpg?v=1687793990', 6, 51, 999.00, 999.00, 'string', 'string', 'string', 'string', 0, '2024-08-12 11:00:15.4504240', '2024-08-12 11:00:15.4504248');

INSERT INTO ProductAttributes (
    [ProductID], 
    [AttributeValueID], 
    [AttributePrice], 
    [AttributeDiscountPrice], 
    [Stock], 
    [Status]
)
VALUES
    (6, 1, 0.00, 0.00, 100, 0),
    (7, 2, 10.00, 10.00, 30, 0),
    (7, 3, 10.00, 10.00, 30, 0),
    (8, 4, 6.00, 6.00, 30, 0),
    (8, 5, 7.00, 7.00, 30, 0),
    (8, 6, 8.00, 8.00, 30, 0),
    (9, 7, 0.00, 0.00, 30, 0),
    (10, 8, 0.00, 0.00, 30, 0),
    (11, 9, 0.00, 0.00, 30, 0),
    (12, 10, 3.00, 3.00, 31, 0),
    (12, 11, 4.00, 4.00, 32, 0),
    (12, 12, 5.00, 5.00, 35, 0),
    (13, 13, 0.00, 0.00, 50, 0),
    (14, 14, 10.00, 10.00, 50, 0),
    (14, 15, 11.00, 11.00, 59, 0),
    (15, 16, 0.00, 0.00, 50, 0),
    (16, 17, 4.00, 4.00, 51, 0),
    (16, 18, 4.00, 4.00, 52, 0),
    (16, 19, 5.00, 5.00, 53, 0),
    (17, 20, 0.00, 0.00, 51, 0),
    (18, 21, 0.00, 0.00, 51, 0),
    (19, 22, 249.00, 249.00, 51, 0),
    (19, 23, 0.00, 0.00, 51, 0),
    (20, 24, 249.00, 249.00, 51, 0),
    (20, 25, 0.00, 0.00, 51, 0),
    (21, 26, 0.00, 0.00, 51, 0);


