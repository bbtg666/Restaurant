const Cart = {
    totalCartItem: 0,

    async addToCart(id, amount) {
        const url = Common.getUrl("/cart/addtocart");
        const data = { id, amount };

        await Common.ajax({
            method: 'post',
            url,
            data
        });
    },

    async addItemToCart(element, id) {
        const [spinerElement] = element.children;
        spinerElement.classList.remove('d-none');
        await Cart.addToCart(id);
        setTimeout(() => {
            spinerElement.classList.add('d-none');
        }, 700)
    },

    async calculatePrice(element, id, price) {
        const amount = Number(element.value);

        if (amount < 0) {
            element.value = 0;
            return;
        }

        const totalElement = document.getElementById(`cartTotalPrice-${id}`);
        await Cart.addToCart(id, amount);
        let total = amount * price;
        totalElement.innerHTML = total;
    },

    async deleteCartItem(id) {
        const url = Common.getUrl("/cart/deletecartitem");
        const data = { id };

        const { isSuccess } = await Common.ajax({
            method: 'post',
            url,
            data
        });

        if (!isSuccess) {
            return;
        }

        const trElement = document.getElementById(`cartItem-${id}`);

        if (!trElement) {
            return;
        }

        trElement.remove();
        Cart.totalCartItem--;

        if (Cart.totalCartItem <= 0) {
            const submitOrderElement = document.getElementById("submitOrder");
            const trEmptyElement = document.getElementById("trEmpty");
            submitOrderElement.remove();
            trEmptyElement.classList.remove('d-none');
        }
    }
};