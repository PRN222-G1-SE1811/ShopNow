function addProduct() {
    let productContainer = document.getElementById("productsContainer");
    let products = document.querySelectorAll(".product");
    let newProduct = products[0].cloneNode(true);

    // Xóa giá trị nhập vào
    newProduct.querySelectorAll("input").forEach(input => input.value = "");

    // Xóa các thuộc tính cũ (để tạo sản phẩm mới có danh sách riêng)
    let attributes = newProduct.querySelector(".attributesContainer");
    attributes.innerHTML = attributes.children[0].outerHTML;

    productContainer.appendChild(newProduct);
}

function removeProduct(button) {
    let product = button.closest(".product");
    let products = document.querySelectorAll(".product");

    if (products.length > 1) {
        product.remove();
    } else {
        alert("Phải có ít nhất một sản phẩm!");
    }
}

function addAttribute(button) {
    let attributesContainer = button.previousElementSibling; // Lấy container chứa attributes
    let attribute = attributesContainer.children[0].cloneNode(true);

    // Xóa giá trị nhập vào của thuộc tính mới
    attribute.querySelectorAll("input").forEach(input => input.value = "");

    attributesContainer.appendChild(attribute);
}

function removeAttribute(button) {
    let attribute = button.closest(".attribute");
    let attributesContainer = attribute.parentElement;

    if (attributesContainer.children.length > 1) {
        attribute.remove();
    } else {
        alert("Phải có ít nhất một thuộc tính!");
    }
}