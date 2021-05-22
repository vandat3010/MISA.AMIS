<template>
  <date-picker
    :value="value"
    :displayFormat="displayFormat"
    :isDateDisabled="isDateDisabled"
    :weekdays="weekdays"
    :months="months"
    @input="$emit('input', $event)"
  >
    <template v-slot:default="{ inputValue, toggle, open, close }">
      <div class="con-input">
        <input
          type="text"
          class="input"
          v-bind="inputAttributes"
          :value="inputValue"
          @focus="open"
          @blur="close"
        />
        <span class="icon-input icon icon-calendar" @click="toggle"></span>
      </div>
    </template>
  </date-picker>
</template>

<script>
import DatePicker from "vue-date-pick";
import "vue-date-pick/dist/vueDatePick.css";
export default {
  components: {
    DatePicker,
  },
  props: {
    /**
     * Các thuộc tính truyền vào cho input.
     * CreatedBy: NVDAT 20/05/2021
     */
    inputAttributes: {
      type: Object,
      default: null,
    },

    /**
     * Giá trị khởi tạo date picker.
     * CreatedBy: NVDAT 20/05/2021
     */
    value: {
      type: String,
      default: null,
    },

    /**
     * Định dạng ngày sẽ hiển thị trong date picker
     * CreatedBy: NVDAT 20/05/2021
     */
    displayFormat: {
      type: String,
      default: null,
    },

    /**
     * Khoảng thời gian không thể chọn
     * CreatedBy: NVDAT 20/05/2021
     */
    isDateDisabled: {
      type: Function,
      default: null,
    },

    /**
     * locale
     * CreatedBy: NVDAT 20/05/2021
     */
    weekdays: {
      type: Array,
      default: () => ["T2", "T3", "T4", "T5", "T6", "T7", "CN"],
    },

    months: {
      type: Array,
      default: () => [
        "Tháng 1",
        "Tháng 2",
        "Tháng 3",
        "Tháng 4",
        "Tháng 5",
        "Tháng 6",
        "Tháng 7",
        "Tháng 8",
        "Tháng 9",
        "Tháng 10",
        "Tháng 11",
        "Tháng 12",
      ],
    },
  },
};
</script>

<style lang="scss" scoped>
.input {
  font-size: 13px;
}
.icon-input {
  top: 1px;
  right: 1px;
  cursor: pointer;
}
</style>