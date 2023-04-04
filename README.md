<div id="Top"></div>

# QUẢN LÝ THƯ VIỆN
Hỗ trợ các thư viện dễ dàng hơn trong quản lý sách và giúp người mượn mua sách thuận lợi hơn.

## Mục lục

 [I. Mở đầu](#Modau)

 [II. Mô tả](#Mota)

> [1. Ý tưởng](#Ytuong)
>
> [2. Công nghệ](#Congnghe)
>
> [3. Người dùng](#Doituongsudung)
>
> [4. Mục tiêu](#Muctieu)
>
> [5. Tính năng](#Tinhnang)

[III. Tác giả](#Tacgia)

[IV. Người hướng dẫn](#Nguoihuongdan)

[V. Tổng kết](#Tongket)


<!-- MỞ ĐẦU -->
<div id="Modau"></div>

## I. Mở đầu
* Sách là một nguồn tài nguyên tuyệt vời cho kiến ​​thức và phát triển cá nhân. Tuy nhiên, đôi khi chúng ta cũng gặp phải những trở ngại nhất định trong việc tìm kiếm cuốn sách phù hợp với mức giá hợp lý nhất. Và người bán hàng gặp khó khăn trong việc quản lý sách và khách hàng với số lượng lớn. Chính vì vậy nhóm đã quyết định phát triển một phần mềm có tên là Masterlibrary để giải quyết các vấn đề trên.

* Hiện tại, Masterlibrary đã trang bị cho mình đầy đủ các thể loại sách đáp ứng mọi nhu cầu của bạn đọc từ giải trí đến học tập hay các dịch vụ như khoa học, nghiên cứu,… Cực kỳ thuận tiện khi mua sách trực tuyến, hỗ trợ phân phối tới 64 tỉnh thành và các thành phố lớn trên cả nước, cá nhân ở gần thư viện cũng có thể sử dụng dịch vụ cho thuê sách hoặc đến thư viện đọc sách. Và quan trọng nhất là nó cũng giúp thủ thư quản lý sách dễ dàng hơn.

<!-- MÔ TẢ -->
<div id="Mota"></div>

## II. Mô tả

<!-- Ý TƯỞNG -->
<div id="Ytuong"></div>

### 1. Ý tưởng
* Với mục tiêu nâng cao trải nghiệm người dùng, công nghệ WPF được áp dụng, ngôn ngữ XAML đáp ứng yêu cầu khắt khe hơn, giao diện cập nhật, hiện đại, trực quan hơn, phù hợp với chuẩn mực hiện hành, ngôn ngữ lập trình dễ hiểu, dễ tiếp cận và dễ dàng tạo và chỉnh sửa giao diện người dùng đồ họa.

* Sử dụng mô hình MVVM để tách giao diện và xử lý, tăng khả năng sử dụng lại các thành phần hoặc thay đổi giao diện chương trình mà không cần viết lại quá nhiều mã, bạn có thể phát triển ứng dụng, nâng cấp, bảo trì, mở rộng hoặc sửa chữa một cách nhanh chóng và dễ dàng.

* Lập trình theo hướng đa luồng (MultiThreading), tối ưu hóa phần cứng, tăng tốc độ xử lý, tăng tốc độ ứng dụng.

* Sử dụng nền tảng quản lý cơ sở dữ liệu đám mây Azure SQL Database để đồng bộ dữ liệu giữa các thiết bị người dùng nhằm đảm bảo tính thống nhất và chính xác của dữ liệu.
* Công nghệ mã hóa Base64 được sử dụng trong quản lý tài khoản người dùng nhằm đảm bảo an toàn trong quá trình sử dụng và hạn chế tối đa tổn thất khi dữ liệu không may bị thất thoát ra bên ngoài.


<div id="Congnghe"></div>

### 2. Công nghệ
* Ngôn ngữ: C# nền tảng .Net FrameWork v4.6.2
* UI Framework: Windows Presentation Foundation (WPF)
* Mô hình MVVM
* IDE: Visual Studio 2022
* Database: SQL Server, Azure SQL Database
* Công cụ quản lý sourcecode: Git, GitHub
* Cloud: Cloudinary
* Khác: Office365, OneDrive, Microsoft Teams, Facebook

<div id="Doituongsudung"></div>

### 3. Đối tượng sử dụng
* Quản lý thư viện hoặc nhân viên thư viện: vai trò quản lý
* Khách hàng

<div id="Muctieu"></div>

### 4. Mục tiêu

* <strong>Ứng dụng thực tế</strong>
    
    * Đáp ứng yêu cầu của khách hàng, hệ thống có tính ổn định cao, dễ sử dụng, không gây khó khăn cho người dùng, được thiết kế đặc biệt cho khách hàng Việt Nam.

    * Được sử dụng rộng rãi trong các hệ thống thư viện, thay thế các ứng dụng cũ còn nhiều hạn chế, giao diện lỗi thời hay hình thức quản lý thủ công truyền thống cồng kềnh, khó quản lý, dễ xảy ra lỗi không đáng có.

    * Trở thành một trong những ứng dụng được khách hàng lựa chọn và tin tưởng.

 * <strong>Yêu cầu ứng dụng</strong>
 
    * Đáp ứng chức năng tiêu chuẩn theo yêu cầu của các ứng dụng quản lý thư viện hiện có trên thị trường. Ngoài ra, các chức năng mới được mở rộng và phát triển nhằm hỗ trợ tối đa người dùng, tự động hóa hoạt động quản lý sân khấu, nhà hát, khắc phục những hạn chế, yếu kém của các hệ thống quản lý hiện tại.

    * Nâng cao tính chính xác và bảo mật trong quản lý thông tin doanh nghiệp và khách hàng.

    * Việc lập báo cáo, thống kê, cập nhật dữ liệu, đồng bộ giữa các máy tính đều phải diễn ra nhanh chóng, chính xác.

    * Dễ dàng tìm kiếm và tra cứu các thông tin liên quan đến sách, sàn, khách hàng... và lịch sử mua hàng của khách hàng.

    * Giao diện thân thiện, dễ sử dụng, bố cục hợp lý, màu sắc hài hòa, tính đồng bộ cao, phân quyền cho người dùng thông qua tài khoản.

    * Ứng dụng phải tương thích với hầu hết các hệ điều hành phổ biến như Windows Vista SP1, Window 8.1, Window 10, v.v.

<div id="Tinhnang"></div>

### 5. Tính năng
* Quản lý đăng nhập, hỗ trợ việc khôi phục tài khoản cho người dùng khi quên mật khẩu.

* Với vai trò quản lý (admin):
    * Thống kê: tính doanh thu của thư viện (có thể lọc theo tháng hoặc theo năm tuỳ nhu cầu).​

    * Quản lý sách: quản lý tất các loại sách có trong thư viện.​

    * Nhập sách: nhập thêm sách cho thư viện.​

    * Lịch sử: ghi lại tất cả các giao dịch xảy ra trong quá trình quản lý (tiền nhập sách, bán sách, thu phạt, thông tin mượn sách, …).​

    * Tầng dãy: lưu lại thông tin vị trí của từng quyển sách để khách hàng có thể dễ dàng tìm sách mình đang cần mà không phải tới quầy để hỏi.​

    * Mượn sách: ghi lại thông tin của người mượn và sách mượn.​

    * Sự cố: tiếp nhận những sự cố được đóng góp bởi khách hang và xử lý tình trạng sự cố.​

    * Cài đặt: thay đổi thông tin quản lý (mật khẩu,...)​

    * Quản lý khách hàng: lưu lại thông tin của tất cả các khách hàng. 
 
* Với vai trò khách hàng:
    * Mua sách: bao gồm tất cả sách có trong thư viện và thông tin sách đang sẵn sàng để được bán đi.​

    * Giỏ hàng: thanh toán tất cả sách đã chọn mua tại giỏ hàng.​

    * Vị trí sách: chứa thông tin vị trí sách trong thư viện giúp khách hàng có thể dễ dàng tìm những cuốn sách theo nhu cầu mà không cần phải đến trực tiếp quầy.​

    * Sách mượn: hiển thị thông tin những cuốn sách khác hàng đang mượn của thư viện.​

    * Báo cáo sự cố: report lại thông tin những thứ cần sửa chữa và nâng cấp để cải thiện trải nghiệm của người dùng tại thư viện.​

    * Cài đặt: đổi mật khẩu, và thay đổi thông tin khách hàng (tên, email, địa chỉ).
<!-- TÁC GIẢ -->
<div id="Tacgia"></div>

## III. Tác giả

* [Huỳnh Mai Cao Nhân](https://github.com/HuynhNhan0330) - 21522401
    * Vai trò: Team learder, frontend developer, backend developer

* [Nguyễn Hoàng Minh](https://github.com/hoangmindrespect) - 21522343
    * Vai trò: Database design, frontend developer, backend developer

* [Ngô Phương Nam](https://github.com/dunoiww) - 21522361
    * Vai trò: Tester, frontend developer, backend developer

* [Phạm Nguyễn](https://github.com/kazei1211) - 21522394
    * Vai trò: UI/UX designer, frontend developer, backend developer

<!-- NGƯỜI HƯỚNG DẪN -->
<div id="Nguoihuongdan"></div>

## IV. Người hướng dẫn
* Giảng viên: Nguyễn Tấn Toàn

<!-- TỔNG KẾT -->
<div id="Tongket"></div>

## V. Tổng kết
* Một sản phẩm là kết quả của một dự án được hoàn thành bởi các thành viên trong nhóm. Qua quá trình này, các thành viên đã có được những kiến ​​thức và kỹ năng chuyên môn nhất định về quy trình lập trình thực tế, hiểu rõ hơn về lập trình, đồng thời tích lũy cho mình những kinh nghiệm và bài học quý báu cho công việc sau này.

* Ngoài ra, nhóm cũng xin gửi lời cảm ơn chân thành và sâu sắc đến thầy giáo hướng dẫn Nguyễn Tấn Toàn đã đồng hành cùng nhóm để đạt được kết quả như ngày hôm nay trong suốt quá trình thực hiện đề tài.

* Sản phẩm của một nhóm có thể để lại nhiều điều mong muốn khi nó được xây dựng và phát triển. Vì vậy, đừng ngần ngại gửi đề xuất hoặc nhận xét của bạn tới email helperCusML@gmail.com. Mỗi đóng góp của bạn sẽ được ghi nhận và là động lực để đội ngũ hoàn thiện sản phẩm hơn nữa.

* Cám ơn vì sự quan tâm của bạn!

---

<p align="right"><a href="#Top">Quay lại đầu trang</a></p>
