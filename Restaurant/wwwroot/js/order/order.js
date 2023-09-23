const Order = {
    async changeStatus(element, id) {
        const status = element.value;
        const url = Common.getUrl("/admin/ordermeal/changestatus");
        const data = { id, status };

        await Common.ajax({
            method: 'post',
            url,
            data
        });
    },

    async showDetail(id) {
        const url = Common.getUrl("/order/detail");
        const data = { id };
            
        const html = await Common.ajax({
            method: 'post',
            url,
            data
        });

        const modal = new bootstrap.Modal(document.getElementById('modal'))
        const modalBody = document.getElementById('modalBody');
        modalBody.innerHTML = html;
        modal.show();
    }
};