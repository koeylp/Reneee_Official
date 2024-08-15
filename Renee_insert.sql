SET
    IDENTITY_INSERT dbo.Categories ON;

INSERT INTO
    Categories (
        [Id],
        [Name],
        [Thumbnail],
        [ThumbnailCate],
        [Description],
        [Status]
    )
VALUES
    (
        1,
        'Lips',
        'https://www.reneecosmetics.in/cdn/shop/files/lips_Pink-Theme_250x250_5c612664-941f-4ac6-a94d-b86630a6e3f6.jpg?v=1710410205',
        'https://www.reneecosmetics.in/cdn/shop/files/Lips_Category_Collection_banner_2c9050c4-a166-4524-a2d6-35325c0b613d.jpg?v=1708410393',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        2,
        'Face',
        'https://www.reneecosmetics.in/cdn/shop/files/Face_Pink-Theme_250x250_1.jpg?v=1710410965',
        'https://www.reneecosmetics.in/cdn/shop/files/Face_Category_Collection_banner.jpg?v=1708410436',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        3,
        'Eyes',
        'https://www.reneecosmetics.in/cdn/shop/files/Eyes_Pink-Theme_250x250_4a19cb50-7c08-44b2-8670-6cab7dae1b78.jpg?v=1710411148',
        'https://www.reneecosmetics.in/cdn/shop/files/Eyes_Category_Collection_banner.jpg?v=1708410384',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        4,
        'Nails',
        'https://www.reneecosmetics.in/cdn/shop/files/nails_Pink-Theme_250x250_9ac8e5e5-d214-4b08-83c2-04571dbabd10.jpg?v=1710410470',
        'https://www.reneecosmetics.in/cdn/shop/files/Nails_Category_Collection_banner.jpg?v=1708410445',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        5,
        'Fragrance',
        'https://www.reneecosmetics.in/cdn/shop/files/Fragrance_Pink-Theme_250x250_171a8426-e318-497b-9816-6c880122a043.jpg?v=1710410647',
        'https://www.reneecosmetics.in/cdn/shop/files/Fragrance_Category_Collection_banner.jpg?v=1708410454',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        6,
        'Gifting',
        'https://www.reneecosmetics.in/cdn/shop/files/Gifting_Pink-Theme_250x250_38e57bb2-c976-45e6-b170-150701f0d231.jpg?v=1710410646',
        'https://www.reneecosmetics.in/cdn/shop/files/Gifting_Category_Collection_banner.jpg?v=1708410463',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        7,
        'Combo',
        'https://www.reneecosmetics.in/cdn/shop/files/Combos_Pink-Theme_250x250_6ae3c002-9f6b-4d9b-b0cc-2b76927b9e8b.jpg?v=1710411065',
        'https://www.reneecosmetics.in/cdn/shop/files/Combos_Category_Collection_banner.jpg?v=1708410426',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    ),
    (
        8,
        'Skin',
        'https://www.reneecosmetics.in/cdn/shop/files/Skin_Pink-Theme_250x250_306049bd-6790-4464-8906-37c0874c0357.jpg?v=1717669497',
        'https://www.reneecosmetics.in/cdn/shop/files/Tools_Category_Collection_banner.jpg?v=1708410497',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Curae interdum ridiculus maecenas pretium iaculis torquent.',
        1
    );

SET
    IDENTITY_INSERT dbo.Categories OFF;

GO
INSERT INTO
    Payments (
        [Method],
        [Description],
        [Status]
    )
VALUES
    (
        'Card',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Augue mauris diam finibus.',
        1
    ),
    (
        'COD',
        'Lorem ipsum odor amet, consectetuer adipiscing elit. Augue mauris diam finibus.',
        1
    );

GO
SET
    IDENTITY_INSERT dbo.Attributes ON;

INSERT INTO
    Attributes (Id, Name, Status)
VALUES
    (1, 'Color', 0),
    (2, 'Size', 0),
    (3, 'Packet', 0);

SET
    IDENTITY_INSERT dbo.Attributes OFF;

GO
SET
    IDENTITY_INSERT dbo.AttributeValues ON;

INSERT INTO
    AttributeValues ([Id], [Value], [AttributeId], [Status])
VALUES
    (1, 'Original', 1, 0),
    (2, 'Fab 5', 1, 0),
    (3, 'Fab 5 Nude', 1, 0),
    (4, 'Clear', 1, 0),
    (5, 'Pink', 1, 0),
    (6, 'Nude', 1, 0),
    (7, '1', 3, 0),
    (8, '1', 3, 0),
    (9, '1', 3, 0),
    (10, 'Earth 01', 1, 0),
    (11, 'Saturn 02', 1, 0),
    (12, 'Venus 03', 1, 0),
    (13, '4 in 1', 1, 0),
    (14, 'Black', 1, 0),
    (15, 'Brown', 1, 0),
    (16, '1', 3, 0),
    (17, 'MTN 01', 1, 0),
    (18, 'French Nails', 1, 0),
    (19, 'BN 01', 1, 0),
    (20, '1', 3, 0),
    (21, '4', 3, 0),
    (22, '50 ML', 2, 0),
    (23, '15 ML', 2, 0),
    (24, '50 ML', 2, 0),
    (25, '15 ML', 2, 0),
    (26, '4', 3, 0),
    (1002, '1', 3, 0);

SET
    IDENTITY_INSERT dbo.AttributeValues OFF;

GO
SET
    IDENTITY_INSERT dbo.Products ON;

INSERT INTO
    Products (
        [Id],
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
    (
        6,
        'RENEE Madness pH Lipstick, 3gm',
        'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-1.jpg?v=1687446357',
        1,
        98,
        499.00,
        499.00,
        '<p><img src="https://cdn.shopify.com/s/files/1/0597/9422/7350/files/VeryMatte_website_01.jpg?v=1700560443"><img src="https://cdn.shopify.com/s/files/1/0597/9422/7350/files/VeryMatte_website_02_480x480.jpg?v=1700560458"><img src="https://cdn.shopify.com/s/files/1/0597/9422/7350/files/VeryMatte_website_03_480x480.jpg?v=1700560472"><img src="https://cdn.shopify.com/s/files/1/0597/9422/7350/files/VeryMatte_website_04_480x480.jpg?v=1700560489"><img src="https://cdn.shopify.com/s/files/1/0597/9422/7350/files/VeryMatte_website_05_480x480.jpg?v=1700560501"></p><p><br></p><ul><li>Rich color pay-off with matte finish</li><li>Velvety smooth finish</li><li>Glides easily with one swipe application</li><li>Long-lasting & feels comfortable on lips</li></ul><p><strong>Give your lips some MATTE-Tude!</strong></p><p>Experience the epitome of lip perfection with the RENEE Very Matte Lipsticks Combo. These slender lipsticks possess an enchanting allure, effortlessly enhancing your lips with a rich, velvety matte finish. The lightweight formula ensures luxurious and long-lasting wear, while the vibrant colors add a touch of elegance to your smile. With four amazing shades, RENEE Very Matte Lipsticks are all set to elevate your lip game to new heights and embrace the sheer sophistication of ultimate lip couture. Indulge yourself in this exquisite experience, and let your lips turn you into a showstopper every time you step out.&nbsp;</p>',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:07:34.4311511',
        '2024-08-12 10:07:34.4311536'
    ),
    (
        7,
        'RENEE FAB 5 Matte Finish 5 in 1 Lipstick 7.5gm',
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-1.jpg?v=1687441106',
        1,
        58,
        750.00,
        750.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:10:30.4500829',
        '2024-08-12 10:10:30.4500859'
    ),
    (
        8,
        'RENEE Hot Lips - Lip Gloss 4.5ml',
        'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_1.jpg?v=1715326888',
        1,
        90,
        250.00,
        250.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:14:40.7265442',
        '2024-08-12 10:14:40.7265505'
    ),
    (
        9,
        'RENEE Madness pH Blush 3gm',
        'https://www.reneecosmetics.in/cdn/shop/files/96A1E17C-10B6-4169-A5DF-3BA09B0F94AE.jpg?v=1695913838',
        2,
        30,
        499.00,
        499.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:18:32.5767301',
        '2024-08-12 10:18:32.5767327'
    ),
    (
        10,
        'RENEE Lumi Glow Cream',
        'https://www.reneecosmetics.in/cdn/shop/files/Lumi-Glow-Listing-PI_01.jpg?v=1718632923',
        2,
        30,
        499.00,
        499.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:20:10.9766623',
        '2024-08-12 10:20:10.9766643'
    ),
    (
        11,
        'RENEE Immortal Face Cream 50 Gm',
        'https://www.reneecosmetics.in/cdn/shop/files/Immortal_01_51cdb96c-28c5-4384-b02a-db7fce32d475.jpg?v=1705383325',
        2,
        30,
        750.00,
        750.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:22:07.1146907',
        '2024-08-12 10:22:07.1146937'
    ),
    (
        12,
        'RENEE Holographic Eyeliner',
        'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_001-min.jpg?v=1722503289',
        3,
        96,
        499.00,
        499.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:38:57.2343436',
        '2024-08-12 10:38:57.2343467'
    ),
    (
        13,
        'RENEE Bold 4 4-IN-1 Kajal',
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_01.jpg?v=1696685279',
        3,
        46,
        599.00,
        599.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:41:15.4164753',
        '2024-08-12 10:41:15.4164778'
    ),
    (
        14,
        'RENEE Superdense Eyebrow Pencil, 0.4 gm',
        'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_01-min.jpg?v=1713505434',
        3,
        109,
        450.00,
        450.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:43:13.9402990',
        '2024-08-12 10:43:13.9403014'
    ),
    (
        15,
        'RENEE Nail Paint Remover, 100 Ml',
        'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_1_-min.jpg?v=1713175558',
        4,
        50,
        199.00,
        199.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:46:03.2674986',
        '2024-08-12 10:46:03.2675001'
    ),
    (
        16,
        'RENEE Stick On Nails',
        'https://www.reneecosmetics.in/cdn/shop/files/renee-stick-on-nails-renee-cosmetics-56.jpg?v=1704374117',
        4,
        156,
        499.00,
        499.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:48:13.8601752',
        '2024-08-12 10:48:13.8601799'
    ),
    (
        17,
        'RENEE Twist & Erase Nail Polish Remover 60ml',
        'https://www.reneecosmetics.in/cdn/shop/files/renee-twist-and-erase-nail-polish-remover-60ml-renee-cosmetics-1.jpg?v=1687794015',
        4,
        51,
        199.00,
        199.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:50:12.4928165',
        '2024-08-12 10:50:12.4928187'
    ),
    (
        18,
        'RENEE Eau De Parfum Pack of 4, 15ml each',
        'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_01.jpg?v=1716182277',
        5,
        1,
        999.00,
        999.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:53:01.8019802',
        '2024-08-12 10:53:01.8019851'
    ),
    (
        19,
        'RENEE Floral Fest Eau De Parfum',
        'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_02-min.jpg?v=1716199577',
        5,
        61,
        350.00,
        350.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:55:39.3770578',
        '2024-08-12 10:55:39.3770585'
    ),
    (
        20,
        'RENEE Citrus Blast Eau De Parfum',
        'https://www.reneecosmetics.in/cdn/shop/files/CitrusBlast_02-min.jpg?v=1716199657',
        5,
        60,
        350.00,
        350.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 10:57:36.2243993',
        '2024-08-12 10:57:36.2243999'
    ),
    (
        21,
        'RENEE Premium Fragrances Set Of 4, 15ml each',
        'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-1_b456b203-0c0b-4fe5-bb7f-c901c18871bf.jpg?v=1687793990',
        6,
        47,
        999.00,
        999.00,
        'string',
        'string',
        'string',
        'string',
        1,
        '2024-08-12 11:00:15.4504240',
        '2024-08-12 11:00:15.4504248'
    ),
    (
        1002,
        'RENEE FAB 10 Combo ( Fab 5 + Fab 5 Nude)',
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-14_48245460-03dd-430e-ae07-f27367a937cf.jpg?v=1700741989',
        7,
        20,
        1500.00,
        1500.00,
        'string',
        'string',
        'string',
        'string',
        0,
        '2024-08-15 09:28:30.6234125',
        '2024-08-15 09:28:30.6234542'
    );

SET
    IDENTITY_INSERT dbo.Products OFF;

SET
    IDENTITY_INSERT dbo.ProductImages ON;

INSERT INTO
    ProductImages (Id, Url, ProductId, Status)
VALUES
    (
        14,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-2.jpg?v=1704954054',
        6,
        1
    ),
    (
        15,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-3.jpg?v=1704954054',
        6,
        1
    ),
    (
        16,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-4.jpg?v=1704954054',
        6,
        1
    ),
    (
        17,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-5.jpg?v=1704954054',
        6,
        1
    ),
    (
        18,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-madness-ph-lipstick-3gm-renee-cosmetics-6.jpg?v=1704954054',
        6,
        1
    ),
    (
        19,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-8.jpg?v=1687441131',
        7,
        1
    ),
    (
        20,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-9.jpg?v=1687441135',
        7,
        1
    ),
    (
        21,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-10.jpg?v=1687441138',
        7,
        1
    ),
    (
        22,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-11.jpg?v=1687441141',
        7,
        1
    ),
    (
        23,
        'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-12.jpg?v=1687441145',
        7,
        1
    ),
    (
        24,
        'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_1.jpg?v=1715326888',
        8,
        1
    ),
    (
        25,
        'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_2.jpg?v=1715326887',
        8,
        1
    ),
    (
        26,
        'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_3.jpg?v=1715326888',
        8,
        1
    ),
    (
        27,
        'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_4.jpg?v=1720264673',
        8,
        1
    ),
    (
        28,
        'https://www.reneecosmetics.in/cdn/shop/files/Renee_Hot_Lips_Rising_Listing_Image_5_102b9744-c5d1-4eff-bc4f-c50abe6ba55c.jpg?v=1720264673',
        8,
        1
    ),
    (
        29,
        'https://www.reneecosmetics.in/cdn/shop/files/96A1E17C-10B6-4169-A5DF-3BA09B0F94AE.jpg?v=1695913838',
        9,
        1
    ),
    (
        30,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_MadnessBlush_ListingPI_06.jpg?v=1695913838',
        9,
        1
    ),
    (
        31,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_MadnessBlush_ListingPI_02.jpg?v=1695913838',
        9,
        1
    ),
    (
        32,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_MadnessBlush_ListingPI_03.jpg?v=1695913838',
        9,
        1
    ),
    (
        33,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_MadnessBlush_ListingPI_04.jpg?v=1695913838',
        9,
        1
    ),
    (
        34,
        'https://www.reneecosmetics.in/cdn/shop/files/Lumi-Glow-Listing-PI_01.jpg?v=1718632923',
        10,
        1
    ),
    (
        35,
        'https://www.reneecosmetics.in/cdn/shop/files/Lumi-Glow-Listing-PI_02.jpg?v=1718632923',
        10,
        1
    ),
    (
        36,
        'https://www.reneecosmetics.in/cdn/shop/files/updated_Lumi-Glow-Listing-PI_03.jpg?v=1720761192',
        10,
        1
    ),
    (
        37,
        'https://www.reneecosmetics.in/cdn/shop/files/Lumi-Glow-Listing-PI_04.jpg?v=1720761227',
        10,
        1
    ),
    (
        38,
        'https://www.reneecosmetics.in/cdn/shop/files/Lumi-Glow-Listing-PI_05.jpg?v=1720761227',
        10,
        1
    ),
    (
        39,
        'https://www.reneecosmetics.in/cdn/shop/files/Immortal_01_51cdb96c-28c5-4384-b02a-db7fce32d475.jpg?v=1705383325',
        11,
        1
    ),
    (
        40,
        'https://www.reneecosmetics.in/cdn/shop/files/Immortal_02.jpg?v=1705383305',
        11,
        1
    ),
    (
        41,
        'https://www.reneecosmetics.in/cdn/shop/files/Immortal_03.jpg?v=1705383306',
        11,
        1
    ),
    (
        42,
        'https://www.reneecosmetics.in/cdn/shop/files/Immortal_04.jpg?v=1705383307',
        11,
        1
    ),
    (
        43,
        'https://www.reneecosmetics.in/cdn/shop/files/Immortal_05.jpg?v=1705383306',
        11,
        1
    ),
    (
        44,
        'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_001-min.jpg?v=1722503289',
        12,
        1
    ),
    (
        45,
        'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_02-min.jpg?v=1722503289',
        12,
        1
    ),
    (
        46,
        'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_03-min.jpg?v=1722503289',
        12,
        1
    ),
    (
        47,
        'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_04-min.jpg?v=1722503289',
        12,
        1
    ),
    (
        48,
        'https://www.reneecosmetics.in/cdn/shop/files/Holographic-Eyeliner-01-SHADE_05-min.jpg?v=1722503289',
        12,
        1
    ),
    (
        49,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_01.jpg?v=1696685279',
        13,
        1
    ),
    (
        50,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_02_c93c857c-09a4-4145-b32e-ad5ca4d1aada.jpg?v=1697810007',
        13,
        1
    ),
    (
        51,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_03.jpg?v=1697810007',
        13,
        1
    ),
    (
        52,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_04_7ced0b0e-cf3f-46ef-8f36-61bfa74222ee.jpg?v=1697810007',
        13,
        1
    ),
    (
        53,
        'https://www.reneecosmetics.in/cdn/shop/files/RENEE_BOLD4KajalPen_ListingPI_05.jpg?v=1697810007',
        13,
        1
    ),
    (
        54,
        'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_01-min.jpg?v=1713505434',
        14,
        1
    ),
    (
        55,
        'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_02-min.jpg?v=1713505434',
        14,
        1
    ),
    (
        56,
        'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_03-min.jpg?v=1713505434',
        14,
        1
    ),
    (
        57,
        'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_04-min.jpg?v=1713505434',
        14,
        1
    ),
    (
        58,
        'https://www.reneecosmetics.in/cdn/shop/files/SuperdenseEyebrowPencil_BLACK_05-min.jpg?v=1713505434',
        14,
        1
    );

(
    59,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_1_-min.jpg?v=1713175558',
    15,
    1
),
(
    60,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_2_-min.jpg?v=1713175558',
    15,
    1
),
(
    61,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_3_-min.jpg?v=1713175558',
    15,
    1
),
(
    62,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_4_-min.jpg?v=1713175558',
    15,
    1
),
(
    63,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_5_-min.jpg?v=1713175558',
    15,
    1
),
(
    64,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-stick-on-nails-renee-cosmetics-56.jpg?v=1704374117',
    16,
    1
),
(
    65,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_2_-min.jpg?v=1713175558',
    16,
    1
),
(
    66,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_3_-min.jpg?v=1713175558',
    16,
    1
),
(
    67,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_4_-min.jpg?v=1713175558',
    16,
    1
),
(
    68,
    'https://www.reneecosmetics.in/cdn/shop/files/8906121649345_5_-min.jpg?v=1713175558',
    16,
    1
),
(
    69,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-twist-and-erase-nail-polish-remover-60ml-renee-cosmetics-1.jpg?v=1687794015',
    17,
    1
),
(
    70,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-twist-and-erase-nail-polish-remover-60ml-renee-cosmetics-2.jpg?v=1687794018',
    17,
    1
),
(
    71,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-twist-and-erase-nail-polish-remover-60ml-renee-cosmetics-3.jpg?v=1687794020',
    17,
    1
),
(
    72,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-twist-and-erase-nail-polish-remover-60ml-renee-cosmetics-4.jpg?v=1687794023',
    17,
    1
),
(
    73,
    'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_01.jpg?v=1716182277',
    18,
    1
),
(
    74,
    'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_02.jpg?v=1716182277',
    18,
    1
),
(
    75,
    'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_03.jpg?v=1716182278',
    18,
    1
),
(
    76,
    'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_04.jpg?v=1716182278',
    18,
    1
),
(
    77,
    'https://www.reneecosmetics.in/cdn/shop/files/Perfume-Pack-of-4-Listing-PI_05.jpg?v=1716182278',
    18,
    1
),
(
    78,
    'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_02-min.jpg?v=1716199577',
    19,
    1
),
(
    79,
    'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_03-min.jpg?v=1714713710',
    19,
    1
),
(
    80,
    'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_04-min.jpg?v=1714713710',
    19,
    1
),
(
    81,
    'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_06-min.jpg?v=1714713711',
    19,
    1
),
(
    82,
    'https://www.reneecosmetics.in/cdn/shop/files/Floral-Fest_07-min.jpg?v=1714713712',
    19,
    1
),
(
    83,
    'https://www.reneecosmetics.in/cdn/shop/files/CitrusBlast_02-min.jpg?v=1716199657',
    20,
    1
),
(
    84,
    'https://www.reneecosmetics.in/cdn/shop/files/newCitrus-Blast_EauDeParfum_50ml.jpg?v=1716553137',
    20,
    1
),
(
    85,
    'https://www.reneecosmetics.in/cdn/shop/files/CitrusBlast_04-min.jpg?v=1716553137',
    20,
    1
),
(
    86,
    'https://www.reneecosmetics.in/cdn/shop/files/CitrusBlast_06-min.jpg?v=1716553137',
    20,
    1
),
(
    87,
    'https://www.reneecosmetics.in/cdn/shop/files/CitrusBlast_07-min.jpg?v=1716553137',
    20,
    1
),
(
    88,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-1_b456b203-0c0b-4fe5-bb7f-c901c18871bf.jpg?v=1687793990',
    21,
    1
),
(
    89,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-2_e9f3ce8a-ec02-426a-8083-1b284d7ba353.jpg?v=1687793992',
    21,
    1
),
(
    90,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-3_678ea999-43df-4c73-938e-daf88ec5a420.jpg?v=1687793996',
    21,
    1
),
(
    91,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-4_7318f087-ecdd-4043-b6b8-447cdddd958a.jpg?v=1687793999',
    21,
    1
),
(
    92,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-premium-fragrances-set-of-4-15ml-each-renee-cosmetics-5_190102dd-bdda-454e-9ef8-f84eaf08e16a.jpg?v=1687794001',
    21,
    1
),
(
    1002,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-14_48245460-03dd-430e-ae07-f27367a937cf.jpg?v=1700741989',
    1002,
    1
),
(
    1003,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-15_dfa1e578-467b-4a69-bf6e-03f13bd07406.jpg?v=1700741989',
    1002,
    1
),
(
    1004,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-16_0c363729-14a7-4ff8-9f7e-a889e390bac1.jpg?v=1700741989',
    1002,
    1
),
(
    1005,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-2_a75d30e7-5cc0-47cf-a2d9-4ea55d7f3768.jpg?v=1700741989',
    1002,
    1
),
(
    1006,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-3_23c2dc7e-38de-4cfc-bd46-e84d0c943fc2.jpg?v=1700741989',
    1002,
    1
),
(
    1007,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-4_d9d792d0-7a78-44c4-aa1e-cc5daf384402.jpg?v=1700741989',
    1002,
    1
),
(
    1008,
    'https://www.reneecosmetics.in/cdn/shop/files/renee-fab-5-matte-finish-5-in-1-lipstick-7-5gm-renee-cosmetics-5_ace44124-4593-4b01-a6ba-7843ed37f3b5.jpg?v=1700741989',
    1002,
    1
);

SET
    IDENTITY_INSERT dbo.ProductImages OFF;

GO
SET
    IDENTITY_INSERT dbo.ProductAttributes ON;

INSERT INTO
    ProductAttributes (
        Id,
        ProductID,
        AttributeValueID,
        AttributePrice,
        AttributeDiscountPrice,
        Stock,
        Status
    )
VALUES
    (1, 6, 1, 0.00, 0.00, 98, 0),
    (2, 7, 2, 10.00, 10.00, 28, 0),
    (3, 7, 3, 10.00, 10.00, 30, 0),
    (4, 8, 4, 6.00, 6.00, 30, 0),
    (5, 8, 5, 7.00, 7.00, 30, 0),
    (6, 8, 6, 8.00, 8.00, 30, 0),
    (7, 9, 7, 0.00, 0.00, 30, 0),
    (8, 10, 8, 0.00, 0.00, 30, 0),
    (9, 11, 9, 0.00, 0.00, 30, 0),
    (10, 12, 10, 3.00, 3.00, 29, 0),
    (11, 12, 11, 4.00, 4.00, 32, 0),
    (12, 12, 12, 5.00, 5.00, 35, 0),
    (13, 13, 13, 0.00, 0.00, 46, 0),
    (14, 14, 14, 10.00, 10.00, 50, 0),
    (15, 14, 15, 11.00, 11.00, 59, 0),
    (16, 15, 16, 0.00, 0.00, 50, 0),
    (17, 16, 17, 4.00, 4.00, 51, 0),
    (18, 16, 18, 4.00, 4.00, 52, 0),
    (19, 16, 19, 5.00, 5.00, 53, 0),
    (20, 17, 20, 0.00, 0.00, 51, 0),
    (21, 18, 21, 0.00, 0.00, 1, 0),
    (22, 19, 22, 249.00, 249.00, 10, 0),
    (23, 19, 23, 0.00, 0.00, 51, 0),
    (24, 20, 24, 249.00, 249.00, 10, 0),
    (25, 20, 25, 0.00, 0.00, 50, 0),
    (26, 21, 26, 0.00, 0.00, 47, 0),
    (1002, 1002, 1002, 0.00, 0.00, 20, 0);

SET
    IDENTITY_INSERT dbo.ProductAttributes OFF;