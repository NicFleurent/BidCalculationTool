<script setup>
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import './assets/bidCalculationTool.css'
</script>

<template>
  <div class="container h-100">
    <header>
      <h1 class="text-white text-center pt-5 pb-3">Bid calcultation tool</h1>
    </header>

    <main>
      <div class="row flex-column">
        <div class="col-12 col-md-8 col-lg-6 offset-md-2 offset-lg-3 form-floating mb-3 p-0">
          <input v-on:input="handleFormChange" type="number" class="form-control" id="basePrice">
          <label for="basePrice">Base price</label>
        </div>
        <div class="col-12 col-md-8 col-lg-6 offset-md-2 offset-lg-3 form-floating mb-3 p-0">
          <select v-on:change="handleFormChange" class="form-select" id="bidType">
            <option selected value="common">Common</option>
            <option value="luxury">Luxury</option>
          </select>
          <label for="bidType">Bid type</label>
        </div>
      </div>
      <div id="resultContainer" class="row mt-3">
        <div v-if="isLoading" class="col-12 col-md-8 col-lg-6 offset-md-2 offset-lg-3 bg-lighBlue rounded px-5 pt-4 pb-5 fs-5">Loading...</div>
        <div v-else-if="error" id="errorResult" class="col-12 col-md-8 col-lg-6 offset-md-2 offset-lg-3 bg-danger rounded px-5 pt-5 pb-5 fs-5 text-center">Error: {{ error }}</div>
        <div v-else-if="bid" class="col-12 col-md-8 col-lg-6 offset-md-2 offset-lg-3 bg-lighBlue rounded px-5 pt-4 pb-5 fs-5">
          <h2 class="mb-3 text-center">Prices and fees</h2>
          <div id="basePriceResult" class="d-flex w-100 justify-content-between">
            <div>Base price :</div>
            <div>{{Number(bid.basePrice).toFixed(2)}}$</div>
          </div>

          <div class="mt-2 fw-bold">Fees</div>
          <div class="ms-3">
            <div id="basicFeeResult" class="d-flex w-100 justify-content-between">
              <div>Basic :</div>
              <div>{{Number(bid.basicFee).toFixed(2)}}$</div>
            </div>
            <div id="specialFeeResult" class="d-flex w-100 justify-content-between">
              <div>Special :</div>
              <div>{{Number(bid.specialFee).toFixed(2)}}$</div>
            </div>
            <div id="associationFeeResult" class="d-flex w-100 justify-content-between">
              <div>Association :</div>
              <div>{{Number(bid.associationFee).toFixed(2)}}$</div>
            </div>
            <div id="storageFeeResult" class="d-flex w-100 justify-content-between">
              <div>Storage :</div>
              <div>{{Number(bid.storageFee).toFixed(2)}}$</div>
            </div>
          </div>
          
          <div id="totalPriceResult" class="d-flex w-100 justify-content-between mt-3">
            <div>Total price :</div>
            <div>{{Number(bid.totalPrice).toFixed(2)}}$</div>
          </div>
        </div>
      </div>
    </main>
  </div>
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
            const errorData = await response.json();
            let errorMessages;
            if(errorData.errors.BasePrice){
              errorMessages = errorData.errors.BasePrice[0];
            }
            else if(errorData.errors.BidType){
              errorMessages = errorData.errors.BidType[0];
            }
            else{
              errorMessages = `HTTP error! Status: ${response.status}`;
            }
            throw new Error(errorMessages);
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

</style>
