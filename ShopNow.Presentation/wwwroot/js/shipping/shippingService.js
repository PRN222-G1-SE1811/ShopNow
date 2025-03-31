$(document).ready(function () {
	// Initialize Nice Select
	$('select').niceSelect();

	// Load Provinces
	$.ajax({
		url: "/Provinces",
		type: "POST",
		dataType: "json",
		success: function (res) {
			let $province = $("#province");
			$province.empty().append('<option value="">-- Select Province/City --</option>');
			res.forEach(province => {
				$province.append(`<option value="${province.provinceID}">${province.provinceName}</option>`);
			});
			$province.niceSelect('update'); // Refresh Nice Select
		},
		error: function () {
			alert("❌ Failed to load provinces!");
		}
	});

	// Load Districts based on selected Province
	$("#province").change(function () {
		let provinceCode = $(this).val();
		let $district = $("#district");
		let $ward = $("#ward");

		// Reset dependent selects
		$district.empty().append('<option value="">-- Select District --</option>').prop("disabled", true);
		$ward.empty().append('<option value="">-- Select Ward/Commune --</option>').prop("disabled", true);
		$district.niceSelect('update');
		$ward.niceSelect('update');

		if (!provinceCode) return;

		// Add loading text
		$district.append('<option value="">Loading...</option>');
		$district.niceSelect('update');

		$.ajax({
			url: `/Districts/${provinceCode}`,
			type: "POST",
			dataType: "json",
			success: function (res) {
				$district.empty().append('<option value="">-- Select District --</option>');
				res.forEach(district => {
					$district.append(`<option value="${district.districtId}">${district.districtName}</option>`);
				});
				$district.prop("disabled", false);
				$district.niceSelect('update'); // Refresh Nice Select
			},
			error: function () {
				alert("❌ Failed to load districts!");
			}
		});
	});

	// Load Wards based on selected District
	$("#district").change(function () {
		let districtCode = $(this).val();
		let $ward = $("#ward");

		$ward.empty().append('<option value="">-- Select Ward/Commune --</option>').prop("disabled", true);
		$ward.niceSelect('update');

		if (!districtCode) return;

		// Add loading text
		$ward.append('<option value="">Loading...</option>');
		$ward.niceSelect('update');

		$.ajax({
			url: `/Wards/${districtCode}`,
			type: "POST",
			dataType: "json",
			success: function (res) {
				$ward.empty().append('<option value="">-- Select Ward/Commune --</option>');
				res.forEach(ward => {
					$ward.append(`<option value="${ward.wardCode}">${ward.wardName}</option>`);
				});
				$ward.prop("disabled", false);
				$ward.niceSelect('update'); // Refresh Nice Select
			},
			error: function () {
				alert("❌ Failed to load wards!");
			}
		});
	});

	// Auto Calculate Shipping Fee when Address is Complete
	$("#ward").change(function () {
		//let provinceID = $("#province").val();
		let districtID = $("#district").val();
		let wardCode = $("#ward").val();

		if (districtID && wardCode) {
			$.ajax({
				url: `/Fee?districtCode=${districtID}&wardCode=${wardCode}`,
				type: "POST",
				dataType: "json",
				contentType: "application/json",
				success: function (res) {
					let formattedFee = new Intl.NumberFormat("en-US").format(res);
					$("#shipping_fee").text(`${formattedFee} VND`);
					$("#ShippingFee").val(res);
					let subtotal = parseCurrency($("#subtotal").text());
					let total = res + subtotal;
					let formattedTotal = new Intl.NumberFormat("en-US").format(total);
					$("#total").text(`${formattedTotal} VND`);
				},
				error: function () {
					alert("❌ Failed to calculate shipping fee!");
				}
			});
		}
	});
});

function parseCurrency(currencyString) {
	return parseInt(currencyString.replace(/,/g, "").replace(/\s?VND/, ""), 10);
}