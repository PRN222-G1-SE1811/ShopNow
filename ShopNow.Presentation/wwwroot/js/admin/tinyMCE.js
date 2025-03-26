function loadEditor(selector) {
	tinymce.init({
		selector: selector,
		plugins: 'link, lists, table',
		menubar: false,
		toolbar: "styleselect | bold italic link bullist numlist | outdent indent | blockquote table undo redo"
	});
}