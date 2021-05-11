<template>
    <div class="navbar">
        <div class="logo-box">
            <div class="toggle-icon"></div>
            <div class="amis-logo"></div>
        </div>
        <div class="navbar-content">
            <a class="item" href="#"
                v-for="(item, index) in items" :key="index"
            >
                <div class="item-icon" :class="item.icon"></div>
                <div class="item-text">{{item.text}}</div>
            </a>
        </div>
    </div>
</template>

<script>
export default {
    name: "Navbar",
    data() {
        return {
            items: [
                {icon: "icon-dashboard", text: "Tổng quan"},
                {icon: "icon-cash", text: "Tiền mặt"},
                {icon: "icon-deposit", text: "Tiền gửi"},
                {icon: "icon-buy", text: "Mua hàng"},
                {icon: "icon-sell", text: "Bán hàng"},
                {icon: "icon-bill-manage", text: "Quản lý hóa đơn"},
                {icon: "icon-storage", text: "Kho"},
                {icon: "icon-tool", text: "Công cụ dụng cụ"},
                {icon: "icon-asset", text: "Tài sản cố định"},
                {icon: "icon-tax", text: "Thuế"},
                {icon: "icon-price", text: "Giá thành"},
                {icon: "icon-synthetic", text: "Tổng hợp"},
                {icon: "icon-budget", text: "Ngân sách"},
                {icon: "icon-report", text: "Báo cáo"},
                {icon: "icon-finance", text: "Phân tích tài chính"},
            ]
        }
    },
}
</script>

<style scoped>
@import url("../../style/layout/navbar.css");
</style>
