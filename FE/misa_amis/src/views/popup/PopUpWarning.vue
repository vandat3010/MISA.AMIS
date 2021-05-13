<template>
  <div class="dialog" :class="{ hide: !isShow }">
    <div class="dialog-background"></div>
    <div class="dialog-content" style="width: 444px">
      <div class="dialog-body">
        <div class="flex-row-align-center" style="padding-top: 32px">
          <div class="icon icon-warning-48"></div>
          <div style="margin-left: 16px">
            {{ message }}
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <div class="dialog-footer-left" v-show="closeBtn.isShow">
          <button class="btn btn-secondary" @click.prevent="onClose">
            {{ closeBtn.label }}
          </button>
        </div>
        <div class="dialog-footer-right" v-show="okBtn.isShow">
          <button class="btn btn-primary" @click.prevent="onOK">
            {{ okBtn.label }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    /**
     * Prop xác định trạng thái dialog.
     * CreatedBy: NVDAT(10/05/2021)
     */
    isShow: {
      type: Boolean,
      default: false,
    },

    /**
     * Prop mã nhân viên.
     * CreatedBy: NVDAT (10/05/2021)
     */
    message: {
      type: String,
      default:
        "Đã có lỗi xảy ra vui lòng liên hệ quản trị viên để được hỗ trợ!",
    },
    closeBtn: {
      type: Object,
      default() {
        return { isShow: true, label: "Đóng" };
      },
    },
    okBtn: {
      type: Object,
      default() {
        return { isShow: true, label: "Ok" };
      },
    },
  },
  methods: {
    /**
     * Phương thức đóng dialog.
     * CreatedBy: NVDAT(10/05/2021)
     */
    onClose() {
      this.$emit("onClose");
    },
    onOK() {
      this.$emit("onOk");
    },
  },
};
</script>
