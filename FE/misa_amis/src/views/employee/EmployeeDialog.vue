<template>
  <div class="dialog" :class="{ hide: !isShow }">
    <div class="dialog-background" @click.prevent="closeDialog"></div>
    <div class="dialog-content">
      <div class="tooltip-box">
        <div class="btn-close-dialog" @click.prevent="closeDialog">
          <div class="icon icon-close"></div>
        </div>
      </div>

      <div class="dialog-header">
        <div class="dialog-title">Thông tin nhân viên</div>
        <div class="option">
          <input type="checkbox" id="is-customer" />
          <label for="is-customer"></label>
          <label style="margin-left: 8px">Là khách hàng</label>
        </div>
        <div class="option">
          <input type="checkbox" id="is-provider" />
          <label for="is-provider"></label>
          <label style="margin-left: 8px">Là nhà cung cấp</label>
        </div>
      </div>

      <div class="dialog-body">
        <div class="row">
          <div class="col-6" style="padding-right: 16px">
            <div class="row">
              <div class="col-5" style="padding-right: 8px">
                <div class="con-input">
                  <label class="label-input"
                    >Mã <span style="color: #f20">*</span></label
                  >
                  <input
                    type="text"
                    class="input"
                    :value="employee && employee.employeeCode"
                    :class="{ 'has-error': errors && errors.employeeCode }"
                    @input="
                      $emit('update:employee', {
                        ...employee,
                        employeeCode: $event.target.value,
                      })
                    "
                    @blur.prevent="validEmployeeCode"
                  />
                  <span
                    v-if="errors && errors.employeeCode"
                    class="text-error"
                    >{{ errors && errors.employeeCode }}</span
                  >
                </div>
              </div>
              <div class="col-7">
                <div class="con-input">
                  <label class="label-input"
                    >Tên <span style="color: #f20">*</span></label
                  >
                  <input
                    type="text"
                    class="input"
                    :class="{ 'has-error': errors && errors.fullName }"
                    :value="employee && employee.fullName"
                    @input="
                      $emit('update:employee', {
                        ...employee,
                        fullName: $event.target.value,
                      })
                    "
                    @blur.prevent="validEmployeeName"
                  />
                  <span v-if="errors && errors.fullName" class="text-error">{{
                    errors && errors.fullName
                  }}</span>
                </div>
              </div>
            </div>
          </div>

          <div class="col-6" style="padding-left: 16px">
            <div class="row">
              <div class="col-5" style="padding-right: 16px">
                <div>
                  <label class="label-input">Ngày sinh</label>
                  <input
                    type="date"
                    class="input"
                    :value="
                      employee && employee.dateOfBirth
                        ? formatYYYMMDD(employee.dateOfBirth)
                        : null
                    "
                    @input="
                      $emit('update:employee', {
                        ...employee,
                        dateOfBirth: $event.target.value,
                      })
                    "
                  />
                </div>
              </div>
              <div class="col-7">
                <div>
                  <label class="label-input">Giới tính</label>
                  <div class="flex-row-align-center" style="height: 32px">
                    <div
                      class="flex-row-align-center"
                      style="margin-right: 16px"
                    >
                      <input
                        type="radio"
                        id="male"
                        class="radio"
                        name="gender"
                        :value="0"
                        :checked="employee && employee.gender == 0"
                        @input="
                          $emit('update:employee', {
                            ...employee,
                            gender: parseInt($event.target.value),
                          })
                        "
                      />
                      <label for="male"></label>
                      <label style="margin-left: 8px">Nữ</label>
                    </div>
                    <div
                      class="flex-row-align-center"
                      style="margin-right: 16px"
                    >
                      <input
                        type="radio"
                        id="female"
                        class="radio"
                        name="gender"
                        :value="1"
                        :checked="employee && employee.gender == 1"
                        @input="
                          $emit('update:employee', {
                            ...employee,
                            gender: parseInt($event.target.value),
                          })
                        "
                      />
                      <label for="female"></label>
                      <label style="margin-left: 8px">Nam</label>
                    </div>
                    <div
                      class="flex-row-align-center"
                      style="margin-right: 16px"
                    >
                      <input
                        type="radio"
                        id="other"
                        class="radio"
                        name="gender"
                        :value="2"
                        :checked="employee && employee.gender == 2"
                        @input="
                          $emit('update:employee', {
                            ...employee,
                            gender: parseInt($event.target.value),
                          })
                        "
                      />
                      <label for="other"></label>
                      <label style="margin-left: 8px">Khác</label>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="col-6" style="padding-right: 16px">
            <div class="con-input">
              <label class="label-input"
                >Đơn vị <span style="color: #f20">*</span></label
              >
              <!-- <select
                class="input"
                :class="{ 'has-error': errors && errors.departmentId }"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    departmentId: $event.target.value,
                  })
                "
                @blur.prevent="validEmployeeDepartmentId"
              >
                <option value="" disabled>
                  Vui lòng chọn đơn vị nhân viên
                </option>
                <option
                  v-for="ed in departments"
                  :key="ed.departmentId"
                  :value="ed.departmentId"
                >
                  {{ ed.departmentName }}
                </option>
              </select> -->
              <model-list-select
                :list="departments"
                v-model="employee.departmentId"
                option-value="departmentId"
                option-text="departmentName"
                placeholder="chọn phòng ban"
              >
              </model-list-select>
              <span v-if="errors && errors.departmentId" class="text-error">{{
                errors && errors.departmentId
              }}</span>
            </div>
          </div>

          <div class="col-6" style="padding-left: 16px">
            <div class="row">
              <div class="col-7" style="padding-right: 8px">
                <div>
                  <label class="label-input">Số CMND</label>
                  <input
                    type="text"
                    class="input"
                    :value="employee && employee.identifyNumber"
                    @input="
                      $emit('update:employee', {
                        ...employee,
                        identifyNumber: $event.target.value,
                      })
                    "
                  />
                </div>
              </div>

              <div class="col-5">
                <div>
                  <label class="label-input">Ngày cấp</label>
                  <input
                    type="date"
                    class="input"
                    :value="
                      employee && employee.dateOfIN
                        ? formatYYYMMDD(employee.dateOfIN)
                        : null
                    "
                    @input="
                      $emit('update:employee', {
                        ...employee,
                        dateOfIN: $event.target.value,
                      })
                    "
                  />
                </div>
              </div>
            </div>
          </div>

          <div class="col-6" style="padding-right: 16px">
            <div>
              <div>
                <label class="label-input">Chức danh</label>
                <input
                  type="text"
                  class="input"
                  :value="employee && employee.positionName"
                  @input="
                    $emit('update:employee', {
                      ...employee,
                      positionName: $event.target.value,
                    })
                  "
                />
              </div>
            </div>
          </div>

          <div class="col-6" style="padding-left: 16px">
            <div>
              <label class="label-input">Nơi cấp</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.placeOfIN"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    placeOfIN: $event.target.value,
                  })
                "
              />
            </div>
          </div>

          <div class="col-12" style="margin-top: 30px">
            <div>
              <label class="label-input">Địa chỉ</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.address"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    address: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px">
            <div>
              <label class="label-input">Điện thoại di động</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.phoneNumber"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    phoneNumber: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px">
            <div>
              <label class="label-input">Điện thoại cố định</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.telephoneNumber"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    telephoneNumber: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px">
            <div>
              <label class="label-input">Email</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.email"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    email: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px"></div>
          <div class="col-3" style="padding-right: 8px">
            <div>
              <label class="label-input">Tài khoản ngân hàng</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.bankAccountNumber"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    bankAccountNumber: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px">
            <div>
              <label class="label-input">Tên ngân hàng</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.bankName"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    bankName: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px">
            <div>
              <label class="label-input">Chi nhánh</label>
              <input
                type="text"
                class="input"
                :value="employee && employee.bankBranchName"
                @input="
                  $emit('update:employee', {
                    ...employee,
                    bankBranchName: $event.target.value,
                  })
                "
              />
            </div>
          </div>
          <div class="col-3" style="padding-right: 8px"></div>
        </div>
      </div>

      <div class="dialog-footer">
        <div class="dialog-footer-left">
          <button class="btn btn-secondary" @click.prevent="closeDialog">
            Hủy
          </button>
        </div>
        <div class="dialog-footer-right">
          <button class="btn btn-secondary" @click.prevent="onClickSave">
            Cất
          </button>
          <button class="btn btn-primary" style="margin-left: 8px">
            Cất và thêm
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import dayjs from "dayjs";
import { ModelListSelect } from "vue-search-select";
export default {
  components: {
    ModelListSelect,
  },
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
     * Prop thông tin nhân viên.
     * CreatedBy: NVDAT(10/05/2021)
     */
    employee: {
      type: Object,
    },
    /**
     * Prop danh sách đơn vị nhân viên.
     * CreatedBy: NVDAT(10/05/2021)
     */
    departments: {
      type: Array,
      default: null,
    },
  },

  data: () => ({
    errors: null,
  }),

  methods: {
    /**
     * Phương thức click lưu thông tin nhân viên.
     */
    onClickSave() {
      if (this.errors) {
        for (let err in this.errors) {
          if (this.errors[err]) {
            return;
          }
        }
      }
      this.$emit("onPositive");
    },

    /**
     * Phương thức đóng dialog
     * CreatedBy: NVDAT(10/05/2021)
     */
    closeDialog() {
      this.$emit("onClose");
    },

    /**
     * Hàm format date về dạng YYYY-MM-DD
     * CreatedBy: NVDAT(10/05/2021)
     */
    formatYYYMMDD(dateStr) {
      return dateStr ? dayjs(dateStr).format("YYYY-MM-DD") : null;
    },

    /**
     * valid mã nhân viên
     */
    validEmployeeCode(e) {
      let val = e.target.value;
      if (!val) {
        this.errors = {
          ...this.errors,
          employeeCode: "Mã nhân viên không được để trống.",
        };
      } else {
        this.errors = {
          ...this.errors,
          employeeCode: null,
        };
      }
    },

    /**
     * valid tên nhân viên
     */
    validEmployeeName(e) {
      let val = e.target.value;
      if (!val) {
        this.errors = {
          ...this.errors,
          fullName: "Tên nhân viên không được để trống.",
        };
      } else {
        this.errors = {
          ...this.errors,
          fullName: null,
        };
      }
    },

    /**
     * valid đơn vị nhân viên
     */
    validEmployeeDepartmentId(e) {
      let val = e.target.value;
      if (!val) {
        this.errors = {
          ...this.errors,
          employeeDepartmentId: "Đơn vị nhân viên không được để trống.",
        };
      } else {
        this.errors = {
          ...this.errors,
          employeeDepartmentId: null,
        };
      }
    },
  },
};
</script>