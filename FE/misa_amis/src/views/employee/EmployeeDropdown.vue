<template>
  <div class="table-option">
    <button class="btn" @click.prevent="onClickBtnEdit">Sửa</button>
    <div class="dropdown">
      <div class="dropdown-btn" @click.prevent="toggleDropdown">
        <button class="btn icon icon-chevron-down-blue"></button>
      </div>
      <div class="dropdown-content right" :class="{ hide: !isShow }">
        <div class="dropdown-item" @click.prevent="onClickReplicate">
          Nhân bản
        </div>
        <div class="dropdown-item" @click.prevent="onClickBtnDel">Xóa</div>
        <div class="dropdown-item">Ngưng sử dụng</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  //#region data
  data: () => ({
    /**
     * Biến xác định trạng thái show dropdown.
     * CreatedBy: NVDAT(10/05/2021)
     */
    isShow: false,
  }),
  //#endregion

  //#region method
  methods: {
    /**
     * Phương thức toggle trạng thái dropdown.
     * CreatedBy: NVDAT(10/05/2021)
     */
    toggleDropdown() {
      this.isShow = !this.isShow;
    },

    /**
     * Phương thức close dropdown.
     * CreatedBy: NVDAT(10/05/2021)
     */
    close(e) {
      if (!this.$el.contains(e.target)) {
        this.isShow = false;
      }
    },

    /**
     * click button edit.
     * CreatedBy: NVDAT(10/05/2021)
     */
    onClickBtnEdit() {
      this.$emit("onClickBtnEdit");
    },
    /**
     * click button nhân bản.
     * CreatedBy: NVDAT(17/05/2021)
     */
    onClickReplicate() {
      this.toggleDropdown();
      this.$emit("onClickBtnReplicate");
    },
    /**
     * Click button xóa.
     * CreatedBy: NVDAT(10/05/2021)
     */
    onClickBtnDel() {
      this.toggleDropdown();
      this.$emit("onClickBtnDel");
    },
  },
  //#endregion method

  //#region mounted
  mounted() {
    document.addEventListener("click", this.close);
  },
  //#endregion
  beforeDestroy() {
    document.removeEventListener("click", this.close);
  },
};
</script>

<style scoped>
.dropdown-btn {
  height: 16px;
}
</style>