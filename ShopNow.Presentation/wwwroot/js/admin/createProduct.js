function addProduct() {
    const productsContainer = document.getElementById("productsContainer");
    const productIndex = productsContainer.children.length;

    const productHtml = `
        <div class="product" data-product-index="${productIndex}">
            <h5>Product Specification</h5>
            <div class="attributesContainer" data-min-attributes="1">
                <div class="form-row attribute mb-3">
                    <div class="col-md-4">
                        <select name="ProductVariantDTOs[${productIndex}].AttributeDTOs[0].Key" 
                                class="form-control" 
                                onchange="updateAvailableAttributes(this)"
                                data-val="true"
                                data-val-required="Attribute must be selected">
                            <option value="color" selected>Color</option>
                        </select>
                    </div>
                    <div class="col-md-4">
                        <input name="ProductVariantDTOs[${productIndex}].AttributeDTOs[0].Value" 
                               type="text" 
                               class="form-control" 
                               placeholder="Enter color"
                               data-val="true"
                               data-val-required="Color is required"
                               data-val-length-min="1"
                               data-val-length-max="50"
                               data-val-length="Color must be between 1 and 50 characters">
                    </div>
                    <div class="col-md-2" style="display:none;">
                        <button type="button" class="btn btn-danger text-white" onclick="removeProductAttribute(this)">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                </div>
            </div>
            <button type="button" class="btn btn-primary mb-3" onclick="addAttribute(this, ${productIndex})">Add attribute</button>
            
            <div class="attributes-validation-error text-danger d-none">
                At least one attribute is required for this product.
            </div>
            
            <div class="mb-3">
                <label for="ProductVariantDTOs_${productIndex}__ProductDetail_Price" class="form-label">Price ($)</label>
                <input id="ProductVariantDTOs_${productIndex}__ProductDetail_Price" 
                       name="ProductVariantDTOs[${productIndex}].ProductDetail.Price" 
                       type="number" 
                       class="form-control" 
                       step="0.01" 
                       required
                       min="0"
                       data-val="true"
                       data-val-required="Price is required"
                       data-val-number="Price must be a valid number"
                       data-val-range-min="0"
                       data-val-range="Price must be greater than or equal to 0">
                <span class="text-danger field-validation-valid" 
                      data-valmsg-for="ProductVariantDTOs[${productIndex}].ProductDetail.Price" 
                      data-valmsg-replace="true"></span>
            </div>
            
            <div class="mb-3">
                <label for="ProductVariantDTOs_${productIndex}__ProductDetail_Quantity" class="form-label">Quantity</label>
                <input id="ProductVariantDTOs_${productIndex}__ProductDetail_Quantity"
                       name="ProductVariantDTOs[${productIndex}].ProductDetail.Quantity" 
                       type="number" 
                       class="form-control" 
                       required
                       min="1"
                       data-val="true"
                       data-val-required="Quantity is required"
                       data-val-number="Quantity must be a valid number"
                       data-val-range-min="1"
                       data-val-range="Quantity must be at least 1">
                <span class="text-danger field-validation-valid" 
                      data-valmsg-for="ProductVariantDTOs[${productIndex}].ProductDetail.Quantity" 
                      data-valmsg-replace="true"></span>
            </div>
            
            <div class="mb-3">
                <label for="ProductVariantDTOs_${productIndex}__ProductDetail_Files" class="form-label">Product Images</label>
                <input id="ProductVariantDTOs_${productIndex}__ProductDetail_Files"
                       name="ProductVariantDTOs[${productIndex}].ProductDetail.Files" 
                       type="file" 
                       class="form-control" 
                       multiple
                       accept="image/*"
                       data-val="true"
                       data-val-fileextensions="Only image files are allowed"
                       data-val-fileextensions-extensions="jpg,jpeg,png,gif,bmp">
                <span class="text-danger field-validation-valid" 
                      data-valmsg-for="ProductVariantDTOs[${productIndex}].ProductDetail.Files" 
                      data-valmsg-replace="true"></span>
            </div>
            
            <button type="button" class="btn btn-danger" onclick="removeProduct(this)">Remove Product</button>
            <hr>
        </div>
    `;

    productsContainer.insertAdjacentHTML('beforeend', productHtml);

    // Validate initial attributes
    validateProductAttributes(productsContainer.lastElementChild);

    // Reinitialize client-side validation if using unobtrusive validation
    if ($.validator && $.validator.unobtrusive) {
        $(productsContainer).removeData("validator")
            .removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(productsContainer);
    }
}

function addAttribute(button, productIndex) {
    const attributesContainer = button.previousElementSibling;
    const attributeIndex = attributesContainer.children.length;

    // Lấy danh sách các attribute đã chọn
    const selectedAttributes = Array.from(attributesContainer.querySelectorAll('select')).map(select => select.value);

    // Danh sách các thuộc tính có thể chọn
    const allAttributes = ["color", "size", "weight"];

    // Lọc ra những thuộc tính chưa được chọn
    const availableAttributes = allAttributes.filter(attr => !selectedAttributes.includes(attr));

    // Nếu không còn thuộc tính nào có thể chọn, không cho thêm mới
    if (availableAttributes.length === 0) {
        alert("All attributes have been selected.");
        return;
    }

    // Tạo HTML cho attribute mới
    const attributeHtml = `
        <div class="form-row attribute mb-3">
            <div class="col-md-4">
                <select name="ProductVariantDTOs[${productIndex}].AttributeDTOs[${attributeIndex}].Key" 
                        class="form-control" 
                        onchange="updateAvailableAttributes(this)"
                        data-val="true"
                        data-val-required="Attribute must be selected">
                    ${availableAttributes.map(attr => `<option value="${attr}">${attr.charAt(0).toUpperCase() + attr.slice(1)}</option>`).join('')}
                </select>
            </div>
            <div class="col-md-4">
                <input name="ProductVariantDTOs[${productIndex}].AttributeDTOs[${attributeIndex}].Value" 
                       type="text" 
                       class="form-control" 
                       placeholder="Enter value"
                       data-val="true"
                       data-val-required="Attribute value is required"
                       data-val-length-min="1"
                       data-val-length-max="50"
                       data-val-length="Attribute value must be between 1 and 50 characters">
            </div>
            <div class="col-md-2">
                <button type="button" class="btn btn-danger text-white" onclick="removeProductAttribute(this)">
                    <i class="fa fa-times"></i>
                </button>
            </div>
        </div>
    `;

    attributesContainer.insertAdjacentHTML('beforeend', attributeHtml);
    updateAvailableAttributes();

    // Validate attributes after adding
    validateProductAttributes(button.closest('.product'));
}

function removeProductAttribute(button) {
    const product = button.closest('.product');
    const attributeRow = button.closest(".attribute");
    const attributesContainer = attributeRow.parentElement;

    // Prevent removing the first (color) attribute
    if (attributeRow.querySelector('select').value === 'color' && attributesContainer.children.length === 1) {
        alert("Cannot remove the color attribute.");
        return;
    }

    attributeRow.remove();
    updateAvailableAttributes();

    // Validate attributes after removing
    validateProductAttributes(product);
}

function validateProductAttributes(productElement) {
    const attributesContainer = productElement.querySelector('.attributesContainer');
    const attributesValidationError = productElement.querySelector('.attributes-validation-error');
    const attributeElements = attributesContainer.querySelectorAll('.attribute');

    // Always true now since color is pre-populated
    attributesValidationError.classList.add('d-none');
    return true;
}

function removeProduct(button) {
    // Get the parent product div
    const productDiv = button.closest(".product");
    
    if (productDiv) {
        productDiv.remove();
    }
}


function updateAvailableAttributes() {
    const products = document.querySelectorAll('.product');

    products.forEach(product => {
        const selectElements = product.querySelectorAll('.attributesContainer select');
        const selectedAttributes = Array.from(selectElements).map(select => select.value);
        const allAttributes = ["color", "size", "weight"];

        selectElements.forEach(select => {
            const currentValue = select.value;
            select.innerHTML = "";

            allAttributes.forEach(attr => {
                if (!selectedAttributes.includes(attr) || attr === currentValue) {
                    select.innerHTML += `<option value="${attr}" ${attr === currentValue ? 'selected' : ''}>${attr.charAt(0).toUpperCase() + attr.slice(1)}</option>`;
                }
            });
        });
    });
}