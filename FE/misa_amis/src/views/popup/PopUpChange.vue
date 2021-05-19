<template>
  <div class="dialog" :class="{ hide: !isShow }">
    <div class="dialog-background"></div>
    <div class="dialog-content" style="width: 444px">
      <div class="dialog-body">
        <div class="flex-row-align-center" style="padding-top: 32px">
          <div class="icon icon-change"></div>
          <div style="margin-left: 16px">
            Dữ liệu đã bị thay đổi. Bạn có muốn cất không?
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <div class="dialog-footer-left">
            <button class="btn btn-secondary" @click.prevent="onCancel">
            Hủy
          </button>
        </div>
        <div class="dialog-footer-right">
            <button class="btn btn-secondary" @click.prevent="closeDialog" style="margin-right:12px">
            Không
          </button>
          <button class="btn btn-primary" @click.prevent="onOk">
            Có
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
     * CreatedBy: dbhuan (09/05/2021)
     */
    isShow: {
      type: Boolean,
      default: false,
    },
    /**
     * Prop mã nhân viên.
     * CreatedBy: dbhuan (10/05/2021)
     */
    employeeCode: {
      type: String,
      default: null,
    },
  },
  methods: {
    /**
     * Phương thức đóng dialog.
     * CreatedBy: dbhuan (09/05/2021)
     */
    closeDialog() {
      this.$emit("onClose");
    },
    onCancel(){
        this.$emit("onCancel");
    },
    onOk(){
      this.$emit("onOK");
    }
  },
};
</script>