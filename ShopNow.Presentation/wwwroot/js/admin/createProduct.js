$(document).ready(function () {
	let variantIndex = $("#productVariantsContainer .product-variant-card").length;

	$("#addProductVariant").click(function () {
		let newVariant = `
						<div class="card mb-3 product-variant-card">
							<div class="card-body">
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label for="ProductVariantDTOs_${variantIndex}__Discount">Discount (%)</label>
											<input name="ProductVariantDTOs[${variantIndex}].Discount"
												   class="form-control"
												   type="number"
												   min="0"
												   max="100"
												   required
												   data-val="true"
												   data-val-required="Discount is required"
												   data-val-range="Discount must be between 0 and 100"
												   data-val-range-min="0"
												   data-val-range-max="100"/>
											<span class="text-danger field-validation-valid"
												  data-valmsg-for="ProductVariantDTOs[${variantIndex}].Discount"
												  data-valmsg-replace="true"></span>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label for="ProductVariantDTOs_${variantIndex}__Quantity">Quantity</label>
											<input name="ProductVariantDTOs[${variantIndex}].Quantity"
												   class="form-control"
												   type="number"
												   min="1"
												   required
												   data-val="true"
												   data-val-required="Quantity is required"
												   data-val-range="Quantity must be at least 1"
												   data-val-range-min="1"/>
											<span class="text-danger field-validation-valid"
												  data-valmsg-for="ProductVariantDTOs[${variantIndex}].Quantity"
												  data-valmsg-replace="true"></span>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6">
										<div class="form-group">
											<label for="ProductVariantDTOs_${variantIndex}__Color">Color</label>
											<input name="ProductVariantDTOs[${variantIndex}].Color"
												   class="form-control"
												   type="text"
												   required
												   data-val="true"
												   data-val-required="Color is required"/>
											<span class="text-danger field-validation-valid"
												  data-valmsg-for="ProductVariantDTOs[${variantIndex}].Color"
												  data-valmsg-replace="true"></span>
										</div>
									</div>
									<div class="col-md-6">
										<div class="form-group">
											<label for="ProductVariantDTOs_${variantIndex}__Size">Size</label>
											<input name="ProductVariantDTOs[${variantIndex}].Size"
												   class="form-control"
												   type="text"
												   required
												   data-val="true"
												   data-val-required="Size is required"/>
											<span class="text-danger field-validation-valid"
												  data-valmsg-for="ProductVariantDTOs[${variantIndex}].Size"
												  data-valmsg-replace="true"></span>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="form-group">
											<label for="ProductVariantDTOs_${variantIndex}__Asset">Asset</label>
											<input name="ProductVariantDTOs[${variantIndex}].Asset"
												   class="form-control"
												   type="file"
												   required
												   data-val="true"
												   data-val-required="Asset is required"/>
											<span class="text-danger field-validation-valid"
												  data-valmsg-for="ProductVariantDTOs[${variantIndex}].Asset"
												  data-valmsg-replace="true"></span>
										</div>
									</div>
								<div class="text-right">
									<button type="button" class="btn btn-danger remove-variant">Remove</button>
								</div>
							</div>
						</div>`;

		$("#productVariantsContainer").append(newVariant);
		variantIndex++;

		// **Rebind validation for new fields**
		$.validator.unobtrusive.parse("#productVariantForm");
	});

	// Remove variant form when "Remove" button is clicked
	$(document).on("click", ".remove-variant", function () {
		$(this).closest(".product-variant-card").remove();
		reIndexVariants(); // Call function to re-index
	});

	function reIndexVariants() {
		$("#productVariantsContainer .product-variant-card").each(function (index) {
			$(this).find("input, label, span[data-valmsg-for]").each(function () {
				let attrName = $(this).attr("name");
				let attrFor = $(this).attr("for");
				let attrDataValmsgFor = $(this).attr("data-valmsg-for");

				if (attrName) {
					let updatedName = attrName.replace(/\[\d+\]/, `[${index}]`);
					$(this).attr("name", updatedName);
				}
				if (attrFor) {
					let updatedFor = attrFor.replace(/\_\d+__/, `_${index}__`);
					$(this).attr("for", updatedFor);
				}
				if (attrDataValmsgFor) {
					let updatedDataValmsgFor = attrDataValmsgFor.replace(/\[\d+\]/, `[${index}]`);
					$(this).attr("data-valmsg-for", updatedDataValmsgFor);
				}
			});
		});

		// Update variant index to maintain count
		variantIndex = $("#productVariantsContainer .product-variant-card").length;
	}
});