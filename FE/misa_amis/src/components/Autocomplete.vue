<template>
  <div class="autocomplete">
   
  </div>
</template>

<style lang="less" scoped>
.autocomplete {
  position: relative;

  input {
      z-index: 5;
  }

  .autocomplete-results {
    bottom: 0;
    position: absolute;
    padding: 0;
    margin: 0;
    border: 1px solid #ccc;
    height: 120px;
    overflow: auto;
    width: 100%;
    z-index: 1;
  }
}
</style>