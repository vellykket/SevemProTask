$('#exampleModal').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget); // Button that triggered the modal
    let modal = $(this);
    modal.find('.modal-title').text('Создать курс')
})