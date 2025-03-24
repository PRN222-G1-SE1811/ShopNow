let index = 1;

function addAttribute() {
	let container = $("#attributesContainer");
	let newRow = `
            <div class="form-row attribute mb-3">
                <div class="col-md-4">
                    <select name="Attributes[${index}].Type" class="form-control">
                        <option value="color">Color</option>
                        <option value="size">Size</option>
                        <option value="weight">Weight</option>
                    </select>
                </div>
                <div class="col-md-4">
                    <input type="text" name="Attributes[${index}].Value" class="form-control" placeholder="Enter value">
                </div>
                <div class="col-md-2">
                    <button type="button" class="btn btn-danger remove-btn" onclick="removeAttribute(this)"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
            </div>`;
	container.append(newRow);
	index++;
}

function removeAttribute(btn) {
	if (index > 1) {
		$(btn).closest(".attribute").remove();
	}
}