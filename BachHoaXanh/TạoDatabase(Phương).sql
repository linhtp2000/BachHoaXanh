use  BachHoaXanhDBMS
GO
--THÊM BẢNG Areas
INSERT INTO Areas Values('1',N'TP.Hồ Chí Minh');
INSERT INTO Areas Values('2',N'TP.Cần Thơ');
--THÊM BẢNG Category
INSERT INTO Categories Values('1',N'RAU 4K');
INSERT INTO Categories Values('2',N'ĐỒ UỐNG');
INSERT INTO Categories Values('3',N'SỮA');
INSERT INTO Categories Values('4',N'BÁNH KẸO');
INSERT INTO Categories Values('5',N'GẠO, THỰC PHẨM KHÔ');
INSERT INTO Categories Values('6',N'ĐỒ DÙNG GIA ĐÌNH');
INSERT INTO Categories Values('7',N'ĐỒ ĐÔNG LẠNH');
INSERT INTO Categories Values('8',N'CHĂM SÓC CÁ NHÂN');
--THÊM BẢNG Classify
INSERT INTO Classifies Values('1',N'Nước ngọt, nước trà','2');
INSERT INTO Classifies Values('2',N'Cà phê, trà khô','2');
INSERT INTO Classifies Values('3',N'Nước yến','2');
INSERT INTO Classifies Values('4',N'Sữa tươi','3')
INSERT INTO Classifies Values('5',N'Sữa chua','3')
INSERT INTO Classifies Values('6',N'Sữa đặc','3')
INSERT INTO Classifies Values('7',N'Sữa bột','4')
INSERT INTO Classifies Values('8',N'Gạo','5')
INSERT INTO Classifies Values('9',N'Đồ hộp','5')
INSERT INTO Classifies Values('10',N'Ngũ cốc, yến mạch','5')
INSERT INTO Classifies Values('11',N'Đồ dùng nhà bếp','6')
INSERT INTO Classifies Values('12',N'Túi đựng rác','6')
INSERT INTO Classifies Values('13',N'Tô, chén, ly','6')
INSERT INTO Classifies Values('14',N'Hộp đựng thực phẩm','6')
INSERT INTO Classifies Values('15',N'Cá, gà, mực viên','7')
INSERT INTO Classifies Values('16',N'Kim chi, dưa muối','7')
INSERT INTO Classifies Values('17',N'Thực phẩm chế biến sẵn','7')
INSERT INTO Classifies Values('18',N'Khăn giấy, khăn ướt','8')
INSERT INTO Classifies Values('19',N'Dầu gội, dầu xả','8')
INSERT INTO Classifies Values('20',N'Sữa tắm, xà bông cục','8')
INSERT INTO Classifies Values('21',N'Lăn xịt khử mùi','8')
--THÊM BẢNG Provide
INSERT INTO Providers Values('1',N'Thực Phẩm Đồng Xanh',N'34/23 Hoàng Ngọc Phách P. Phú Thọ Hòa Quận Tân Phú','0936685268') -- 
INSERT INTO Providers Values('2',N'Thực Phẩm Duy Anh',N'368/4 Tỉnh Lộ 15, Ấp Bến Cỏ, Phú Hòa Đông, Tp. Hồ Chí Minh','0903 646 487')
INSERT INTO Providers Values('3',N'San Hà Food',N'Số 951 Tạ Quang Bửu, P. 6, Q. 8, Tp. Hồ Chí Minh','0909.884.159')
INSERT INTO Providers Values('4',N'Xuất Nhập Khẩu Ngọc Liên',N'72 Trần Tấn, P. Tân Sơn Nhì, Q. Tân Phú, Tp. Hồ Chí Minh','(028) 38105182')
--THÊM BẢNG Products
--Id,Name,Price,Discount, Amount, Detail, Image, ClassifyId, BranchId, ProviderId
INSERT INTO Products Values('1',N'Lốc 6 lon Tonic Evervess 330ml',42000, 0, 25,N'Tonic water là hỗn hợp soda (carbonate) + quinine (một loại thuốc chuyên để chữa bệnh sốt rét). Tonic không pha có vị đắng, thường được dùng để pha với rượu.','1','1','1','~/Images/1.1.jpg','~/Images/1.2.jpg','~/Images/1.3.jpg')
INSERT INTO Products Values('2',N'6 chai nước bù khoáng Revive chanh muối 390ml',42000, 0,34,N'Lắc nhẹ trước khi uống, dùng ngay sau khi mở nắp. Ngon hơn khi uống lạnh. Bảo quản Để nơi khô ráo, thoáng mát, tránh ánh sáng trực tiếp hoặc nơi có nhiệt độ cao.','1','1','1','~/Images/2.1.jpg','~/Images/2.2.jpg','~/Images/2.3.jpg')
INSERT INTO Products Values('3',N'Nước giải khát Want Want Frozen vị yogurt cây 78ml', 3200, 0, 30,N'Sản phẩm sản xuất chung trên dây chuyền sản xuất các sản phẩm khác, sản phẩm có thể có chứa sữa','1','1','1','~/Images/3.1.jpg','~/Images/3.2.jpg','~/Images/3.3.jpg')
INSERT INTO Products Values('4',N'Nước tăng lực Monster Energy 355ml',250000,0,44,N'Nước tăng lực Monster Energy chứa Taurine và Nhân sâm, cung cấp cho cơ thể một lượng sinh lực dồi dào và minh mẫn trong suốt thời gian còn lại trong ngày, rất lí tưởng với những công việc đòi hỏi tập trung cao hay phải di chuyển nhiều.','1','1','2','~/Images/4.1.jpg','~/Images/4.2.jpg','~/Images/4.3.jpg')
INSERT INTO Products Values('5',N'Cà phê Cappuccino NesCafé vị caramel 200g',40000,0,30,N'UỐNG NÓNG Hoà 1 gói cafe với 100ml nước nóng, khuấy đều và thưởng thức.UỐNG LẠNH Hoà 1 gói cà phê với 50ml nước nóng. Thêm đá, khuấy đều và thưởng thức','2','1','1','~/Images/5.1.jpg','~/Images/5.2.jpg','~/Images/5.3.jpg')
INSERT INTO Products Values('6',N'Trà đen Lipton nhãn vàng 50g',30000,0,50,N'Trà túi lọc Lipton nhãn vàng được chế biến từ 100% từ búp trà đen, mang đến một sản phẩm có vị đậm đà của trà truyền thống và giữ nguyên được những thành phần tốt cho sức khỏe.','2','1','1','~/Images/6.1.jpg','~/Images/6.2.jpg','~/Images/6.3.jpg')
INSERT INTO Products Values('7',N'Trà bí đao Fuze Tea la hán quả 330ml',8000,0,60,N'Để nơi khô ráo, thoáng mát, tránh ánh sáng trực tiếp hoặc nơi có nhiệt độ cao.','2','1','2','~/Images/7.1.jpg','~/Images/7.2.jpg','~/Images/7.3.jpg')
INSERT INTO Products Values('8',N'Thùng 48 chai nước yến sào đông trùng hạ thảo Green Bird 185ml',1320000,0,44,'Nam giới, người làm việc, người chơi thể thao mỗi ngày cần được bổ sung dinh dưỡng và tăng cường hô hấp. Yến sào và đông trùng hạ thảo là sự kết hợp độc đáo giúp cơ thể duy trì sức khoẻ mỗi ngày','3','1','1','~/Images/8.1.jpg','~/Images/8.2.jpg','~/Images/8.3.jpg')
INSERT INTO Products Values('9',N'Hộp 6 hũ nước yến hoa cúc Song Yến 70ml',361000,0,55,N'Sự hòa quyện hoàn hảo giữa các nguyên liệu hoàn toàn tự nhiên, mang đến hương vị ngọt thanh, thơm dịu cho người thưởng thức','3','1','2','~/Images/9.1.jpg','~/Images/9.2.jpg','~/Images/9.3.jpg')
INSERT INTO Products Values('10',N'Đông trùng hạ thảo chưng sẵn Yến Việt 65ml',47000,0,65,N'Đông Trùng Hạ Thảo YenViet được nuôi cấy tự nhiên với nguồn giống hàng đầu Nhật Bản được chưng với đường phèn ngọt mát giúp bồi bổ sức khỏe và hỗ trợ ngăn ngừa lão hóa hiệu quả.','3','1','2','~/Images/10.1.jpg','~/Images/10.2.jpg','~/Images/10.3.jpg')
INSERT INTO Products Values('11',N'Thùng 48 hộp sữa tươi tiệt trùng có đường Lothamilk 180ml',336000,0,42,N'Lothamilk áp dụng phương pháp làm sữa truyền thống của châu Âu, tuyệt đối KHÔNG dùng chất bảo quản và phụ gia kém an toàn, đạt tiêu chuẩn ISO 22000:2005. Sản phẩm sữa tươi tiệt trùng có đường Lothamilk sử dụng nguồn nguyên liệu siêu sạch được lấy từ những đàn bò khỏe mạnh, được chăm sóc, nuôi dưỡng theo chế độ hợp lý','4','1','3','~/Images/11.1.jpg','~/Images/11.2.jpg','~/Images/11.3.jpg')
INSERT INTO Products Values('12',N'Sữa tươi nguyên kem không đường Inex hộp 1 lít',37000,0,66,N'Sữa tươi nguyên kem Inex được tuyển chọn từ các trang trại bò sữa tại Bỉ với khí hậu ôn hoà và ngành chăn nuôi phát triển. Sữa bò nơi đây luôn được đánh giá cao về chất lượng, trải qua quy trình sản xuất và kiểm soát chất lượng nghiêm ngặt của châu Âu, đảm bảo không chứa chất bảo quản và hormone tăng trưởng.','4','1','3','~/Images/12.1.jpg','~/Images/12.2.jpg','~/Images/12.3.jpg')
INSERT INTO Products Values('13',N'Sữa tươi nguyên kem không đường Pure Milk hộp 1 lít',35000,0,52,N'Sây chuyền sản xuất hiện đại, khép kín, điểm đặc biệt chính là sữa được khử vi trùng bằng phương pháp nấu sữa ở nhiệt độ 135°C -150°C ngay sau đó làm lạnh đột ngột ở 12,5 độ C giúp sữa được tiệt trùng, đảm bảo an toàn với sức khỏe người tiêu dùng','4','1','4','~/Images/13.1.jpg','~/Images/13.2.jpg','~/Images/13.3.jpg')
INSERT INTO Products Values('14',N'Lốc 4 hộp sữa tiệt trùng có đường Nestlé NutriStrong 180ml',28000,0,44,N'Sữa tiệt trùng Nestlé với công thức độc quyền Nutristrong là thành quả nghiên cứu nhiều năm của Nestlé về thể trạng, tình trạng dinh dưỡng của trẻ em Việt Nam.','14','4','1','3','~/Images/14.1.jpg','~/Images/14.2.jpg','~/Images/14.3.jpg')
INSERT INTO Products Values('15',N'Thùng 12 hộp sữa tươi nguyên chất không đường Vinamilk 100% Organic',633000,0,66,N'Chứa nhiều vitamin, khoáng chất không chỉ là nguồn thực phẩm mà còn là nguồn dinh dưỡng không thể thiếu. Vừa giúp xương chắc khỏe, tăng sức đề kháng vừa cung cấp năng lượng cần thiết cho cơ thể.','5','1','2','~/Images/15.1.jpg','~/Images/15.2.jpg','~/Images/15.3.jpg')
INSERT INTO Products Values('16',N'Lốc 6 chai sữa chua uống cam SuSu IQ 80ml',25500,0,56,N'Sử dụng ngay sau khi mở bao bì. Ngon hơn khi dùng lạnh. Không dùng cho bé dưới 1 tuổi. Nên dùng 2-3 chai sữa mỗi ngày. Bảo quản nơi khô ráo thoáng mát.','5','1','3','~/Images/16.1.jpg','~/Images/16.2.jpg','~/Images/16.3.jpg')
INSERT INTO Products Values('17',N'Lốc 4 hộp sữa chua uống vị lựu YoMost 170ml',28000,0,67,N'Lắc đều trước khi uống. Ngon hơn khi uống lạnh. Sử dụng cho 1 lần uống.','5','1','4','~/Images/17.1.jpg','~/Images/17.2.jpg','~/Images/17.3.jpg')
INSERT INTO Products Values('18',N'Lốc 4 chai sữa chua uống hương việt quất Vinamilk Probi 130ml',35000,0,70,N'Thành phần:Nước, đường tinh luyện, sữa bột (3.3%), xirô fructoza, chất ổn định (405, 466), chiết xuất cà rốt tím, hương việt quất tổng hợp tự nhiên và giống tự nhiên dùng cho thực phẩm, probiotic Lactobacitius paracasel LCASEI 431, vitamin D3','5','1','2','~/Images/18.1.jpg','~/Images/18.2.jpg','~/Images/18.3.jpg')
INSERT INTO Products Values('19',N'Sữa chua uống tổ yến Nestlé Yogu gói 85ml',5000,0,30,N'Bảo quản nơi khô ráo, thoáng mát. Tránh ánh nắng trực tiếp. Dùng ngon hơn khi uống lạnh. Thành phần sản phẩm có thể bị lắng, nhưng chất lượng không thay đổi. Lắc đều trước khi sử dụng','5','1','1','~/Images/19.1.jpg','~/Images/19.2.jpg','~/Images/19.3.jpg')
INSERT INTO Products Values('20',N'Sữa chua uống hương kem dâu LiF Kun túi 110ml',4500,0,23,N' Sữa lên men tự nhiên (nước, sữa bò tươi, bột sữa, chất béo sữa, men Streptococcus và Lactobacillus), đường, mạch nha, chất ổn định dùng cho thực phẩm (E466, E440), hương kem dâu tổng hợp,','5','1','3','~/Images/20.1.jpg','~/Images/20.2.jpg','~/Images/20.3.jpg')
INSERT INTO Products Values('21',N'Kem đặc có đường Daily lon 380g',16500,0,34,N'Thành phần: Đường kính (183.3g), nước, sữa (bột whey, bột sữa 44.5g), dầu thực vật, maltodextrin, chất ổn định (E1450, E451i, E322), hương liệu tổng hợp tự nhiên dùng cho thực phẩm, màu thực phẩm (E171)','6','1','3','~/Images/21.1.jpg','~/Images/21.2.jpg','~/Images/21.3.jpg')
INSERT INTO Products Values('22',N'Kem đặc có đường Nuti đỏ hộp 1,284kg',57500,0,45,N'Thành phần: Đường tinh luyện (47%), nước, chất béo thực vật, sữa bột, whey bột, lactoza, chất ổn định và nhũ hóa (E322, E460, E401, E466). Có chứa sữa, lecithin đậu nành','22','6','1','4','~/Images/22.1.jpg','~/Images/22.2.jpg','~/Images/22.3.jpg')
INSERT INTO Products Values('23',N'Kem đặc có đường Lamosa lon 1kg',36000,0,47,N'Thành phần:  Đường kính, nước, sữa bột, dầu thực vật (dầu cọ), chất ổn định (E145, E339(i), E407), vitamin (B1, A, D3)','6','1','1','~/Images/23.1.jpg','~/Images/23.2.jpg','~/Images/23.3.jpg')
INSERT INTO Products Values('24',N'Kem đặc có đường Ngôi sao Phương Nam xanh lá hộp 1,284kg',60000,0,28,N'Thích hợp: Pha cà phê, xay sinh tố, làm sữa chua, bánh flan...','6','1','4','~/Images/24.1.jpg','~/Images/24.2.jpg','~/Images/24.3.jpg')
INSERT INTO Products Values('25',N'Kem đặc có đường Hoàn Hảo lon 380g',17000,0,7,N'Có thể sử dụng cho nhiều mục đích khác nhau: Uống như một loại sữa khi khuấy với nước ấm đã đun sôi, dùng với bánh mì, làm yaourt, pha cà phê sữa, làm sinh tố,…','6','1','3','~/Images/25.1.jpg','~/Images/25.2.jpg','~/Images/25.3.jpg')
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(
--INSERT INTO Products Values(

----THÊM BẢNG EMPLOYEE --***********DBMS
--INSERT INTO Employees VALUES('1',N'Đào Thị Tuyết','daothi','thituyet@gmail.com','0976448593','3','','',24000)
--INSERT INTO Employees VALUES('2',N'Trần Minh Hải','tranhai','haicute@gmail.com','0903748334','3','',24000)
--INSERT INTO Employees VALUES('3',N'Nguyễn Thị Hoa','hoathi','nguyenhoa123@gmail.com','0905920355','','',30000)
--INSERT INTO Employees VALUES('4',N'Lê Ánh Tuyết','anhtuyet','tuyettinh@gmail.com','0901223794','3','',22000)



--THÊM BẢNG EMPLYEE --******MVC
INSERT INTO Employees VALUES('1',N'Đào Thị Tuyết','daothi','thituyet@gmail.com','3','',24000,'0976448593','1')
INSERT INTO Employees VALUES('2',N'Trần Minh Hải','tranhai','haicute@gmail.com','3','',24000,'0903748334','1')
INSERT INTO Employees VALUES('3',N'Nguyễn Thị Hoa','hoathi','nguyenhoa123@gmail.com','','',30000,'0905920355','1')
INSERT INTO Employees VALUES('4',N'Lê Ánh Tuyết','anhtuyet','tuyettinh@gmail.com','3','',22000,'0901223794','1')
INSERT INTO Employees VALUES('5',N'Bùi Vinh Hảo','haohao','hao123@gmail.com','','',30000,'0901453794','2')
INSERT INTO Employees VALUES('6',N'Trần Thiên Thư','thienthu','thuthien@gmail.com','8','',24000,'0998453794','2')
INSERT INTO Employees VALUES('7',N'Hồ Hoàng Minh Ái','aiminh','hoai@gmail.com','8','',24000,'0977453794','2')
INSERT INTO Employees VALUES('8',N'Bác Ý Nhi','ynhi1','ynhib@gmail.com','8','',22000,'0901231794','2')
INSERT INTO Employees VALUES('9',N'Hoàng Minh Thiên Nhật','thienthien','nhatthien@gmail.com','3','',22000,'0901453724','2')
INSERT INTO Employees VALUES('10',N'Trần Anh Nhã','anhnhaa','anhnhat@gmail.com','8','',22000,'0901453276','2')

--THÊM BẢNG BRANCH
--Số 34 Đường số 4D, Khu phố 3, Phường Linh Xuân, Quận Thủ Đức
--Số 244 Huỳnh Văn Bánh, Phường 11, Quận Phú Nhuận
INSERT INTO Branches VALUES('1',N'Chi nhánh 1',N'Số 581 và 583 Kha Vạn Cân, Phường Linh Đông, Quận Thủ Đức, TP.HCM','1','3')
INSERT INTO Branches VALUES('2',N'Ấp Trường Ninh 1, Xã Trường Xuân A, Huyện Thới La','2','8')
--INSERT INTO Branches VALUES('3',N'Số 34 Nguyễn Thông, KV1, Phường An Thới, Quận Bình Thuỷ','2','3')






