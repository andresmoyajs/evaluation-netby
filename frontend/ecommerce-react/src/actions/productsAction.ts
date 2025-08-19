import { config } from '../constants/AppConstant'
import type { ApiResponse } from '@/types/api';
import axios from '../utilities/axiosProducts'


const BASE_URL = config.url.API_URL_PRODUCTS;

export const createProduct = async (formData: FormData): Promise<void> => {
  try {
    await axios.post(`${BASE_URL}/api/v1/product/create`, formData, {
      headers: {
        "Content-Type": "multipart/form-data", // importante para subir archivos
      },
    });
  } catch (error: any) {
    console.error("Error creando producto:", error.response?.data || error.message);
    throw new Error(error.response?.data?.message || "Error creando el producto");
  }
};

export const updateProduct = async (formData: FormData): Promise<void> => {
  try {
    await axios.put(`${BASE_URL}/api/v1/product/update`, formData, {
      headers: {
        "Content-Type": "multipart/form-data", // importante para subir archivos
      },
    });
  } catch (error: any) {
    console.error("Error creando producto:", error.response?.data || error.message);
    throw new Error(error.response?.data?.message || "Error creando el producto");
  }
};


export const fetchProducts = async ({pageIndex, pageSize, search }: {
    pageIndex: number,
    pageSize: number,
    search?: string
}
): Promise<ApiResponse> => {
    try {
        const params = {
            pageIndex,
            pageSize,
            search,
        };

        const { data } = await axios.get<ApiResponse>(
            `${BASE_URL}/api/v1/product/pagination`,
            { params }
        );

        return data;
    } catch (error: any) {
        console.error("Error fetching products:", error.response?.data || error.message);
        throw new Error(error.response?.data?.message || "Error fetching products");
    }
};


export const deleteProduct = async (productId: number): Promise<void> => {
  try {
    await axios.delete(`${BASE_URL}/api/v1/product/${productId}`);
  } catch (error: any) {
    console.error(
      "Error deleting product:",
      error.response?.data || error.message
    );
    throw new Error(
      error.response?.data?.message || "Error eliminando el producto"
    );
  }
};

export const changeStatusProduct = async (productId: number): Promise<void> => {
  try {
    await axios.delete(`${BASE_URL}/api/v1/product/status/${productId}`);
  } catch (error: any) {
    console.error(
      "Error deleting product:",
      error.response?.data || error.message
    );
    throw new Error(
      error.response?.data?.message || "Error eliminando el producto"
    );
  }
};

export const buyProduct = async (productId: number, quantity: number) => {
  const formData = new FormData();
  formData.append("Id", productId.toString());
  formData.append("Stock", quantity.toString());

  try {
    await axios.post(`${BASE_URL}/api/v1/product/buy`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  } catch (error: any) {
    console.error("Error comprando producto:", error.response?.data || error.message);
    throw new Error(error.response?.data?.message || "Error comprando el producto");
  }
};


export const sellProduct = async (productId: number, quantity: number) => {
  const formData = new FormData();
  formData.append("Id", productId.toString());
  formData.append("Stock", quantity.toString());

  try {
    await axios.post(`${BASE_URL}/api/v1/product/sell`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  } catch (error: any) {
    console.error("Error comprando producto:", error.response?.data || error.message);
    throw new Error(error.response?.data?.message || "Error comprando el producto");
  }
};
