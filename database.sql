1. Bảng users (Quản lý người dùng)
CREATE TABLE users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    role VARCHAR(20) CHECK (role IN ('Owner', 'Staff', 'Customer', 'Manager', 'Accountant', 'WarehouseKeeper')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);
2.Bảng products (Quản lý sản phẩm)
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(255) NOT NULL,
    description TEXT,
    price DECIMAL(10,2) NOT NULL,
    stock_quantity INT NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);
3. Bảng categories (Phân loại sản phẩm)
CREATE TABLE categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(100) NOT NULL UNIQUE
);
4. Bảng orders (Quản lý đơn hàng)
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT,
    order_status VARCHAR(20) CHECK (order_status IN ('Pending', 'Completed', 'Cancelled')) NOT NULL,
    payment_status VARCHAR(20) CHECK (payment_status IN ('Pending', 'Paid', 'Failed')) NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES users(user_id)
);
5. Bảng order_items (Chi tiết đơn hàng)
CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    product_id INT,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
6. Bảng reviews (Đánh giá sản phẩm)
CREATE TABLE reviews (
    review_id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    customer_id INT,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    review_text TEXT,
    review_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES products(product_id),
    FOREIGN KEY (customer_id) REFERENCES users(user_id)
);
7. Bảng inventory (Quản lý kho)
CREATE TABLE inventory (
    inventory_id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    stock_quantity INT NOT NULL,
    last_update DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
8. Bảng financial_reports (Báo cáo tài chính)
CREATE TABLE financial_reports (
    report_id INT PRIMARY KEY IDENTITY(1,1),
    report_type VARCHAR(20) CHECK (report_type IN ('Sales', 'Expenses')) NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    report_date DATETIME DEFAULT GETDATE()
);
9. Bảng employees (Quản lý nhân viên)
CREATE TABLE employees (
    employee_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    position VARCHAR(20) CHECK (position IN ('Manager', 'Accountant', 'WarehouseKeeper')) NOT NULL,
    hire_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
10. Bảng categories_products (Bảng trung gian giữa sản phẩm và phân loại)
CREATE TABLE categories_products (
    category_product_id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    category_id INT,
    FOREIGN KEY (product_id) REFERENCES products(product_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
