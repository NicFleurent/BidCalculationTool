<script setup>
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
</script>

<template>
  <header>
    <h1>Bid calcultation tool</h1>
  </header>

  <main>
    <div class="form-floating mb-3">
      <input v-on:input="handleFormChange" type="number" class="form-control" id="basePrice">
      <label for="basePrice">Base price</label>
    </div>
    <div class="form-floating">
      <select v-on:change="handleFormChange" class="form-select" id="bidType">
        <option selected value="common">Common</option>
        <option value="luxury">Luxury</option>
      </select>
      <label for="bidType">Bid type</label>
    </div>

    <div v-if="isLoading">Loading...</div>
    <div v-else-if="error" class="text-danger">Error: {{ error }}</div>
    <div v-else-if="bid" class="mt-5 fs-5">
      <div class="d-flex w-100 justify-content-between">
        <div>Base Price :</div>
        <div>{{Number(bid.basePrice).toFixed(2)}}$</div>
      </div>

      <div class="mt-2 fw-bold">Fees</div>
      <div class="ms-3">
        <div class="d-flex w-100 justify-content-between">
          <div>Basic :</div>
          <div>{{Number(bid.basicFee).toFixed(2)}}$</div>
        </div>
        <div class="d-flex w-100 justify-content-between">
          <div>Special:</div>
          <div>{{Number(bid.specialFee).toFixed(2)}}$</div>
        </div>
        <div class="d-flex w-100 justify-content-between">
          <div>Association :</div>
          <div>{{Number(bid.associationFee).toFixed(2)}}$</div>
        </div>
        <div class="d-flex w-100 justify-content-between">
          <div>Storage :</div>
          <div>{{Number(bid.storageFee).toFixed(2)}}$</div>
        </div>
      </div>
      
      <div class="d-flex w-100 justify-content-between mt-3">
        <div>Total Price :</div>
        <div>{{Number(bid.totalPrice).toFixed(2)}}$</div>
      </div>
    </div>
  </main>
</template>

<script>
  export default {
    data() {
      return {
        bid: null,
        isLoading: false,
        error: null,
        baseURL: "https://localhost:7277"
      };
    },
    methods: {
      handleFormChange(){
        const basePriceElement = document.getElementById("basePrice")
        const basePrice = basePriceElement.value;

        const bidTypeElement = document.getElementById("bidType")
        const bidType = bidTypeElement.value;

        if(basePrice && bidType){
          const bidRequest = {
            "basePrice": basePrice,
            "bidType": bidType
          };

          this.fetchBid(bidRequest);
        }
        else{
          this.bid = null;
        }
      },

      async fetchBid(bidRequest) {
        this.isLoading = true;
        this.error = null;

        try {
          const response = await fetch(this.baseURL + "/Bid", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(bidRequest),
          });

          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }

          this.bid = await response.json();
        } catch (err) {
          this.error = err.message;
        } finally {
          this.isLoading = false;
        }
      },
    },
  };
</script>

<style scoped>
@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
