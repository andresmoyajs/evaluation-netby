const prod = {
  url: {
    API_URL_PRODUCTS: `http://localhost:5002`,
    API_URL_TRANSACTIONS: `http://localhost:5001`,
  },
};

const dev = {
  url: {
    API_URL_PRODUCTS: `http://localhost:5002`,
    API_URL_TRANSACTIONS: `http://localhost:5001`,
  },
};

export const config = process.env.NODE_ENV === "development" ? dev : prod;
