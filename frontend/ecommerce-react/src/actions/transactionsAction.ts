import type { Transaction } from "@/types/transaction";
import axios from "../utilities/axiosTransactions";
import { config } from "@/constants/AppConstant";

const BASE_URL = config.url.API_URL_TRANSACTIONS;


export const getProductTransactions = async (
  productId: number
): Promise<Transaction[]> => {
  try {
    const { data } = await axios.get<Transaction[]>(
      `${BASE_URL}/api/v1/transaction/product/${productId}`
    );
    return data;
  } catch (error: any) {
    console.error(
      "Error fetching transactions:",
      error.response?.data || error.message
    );
    throw new Error(
      error.response?.data?.message || "Error obteniendo el historial de transacciones"
    );
  }
};

